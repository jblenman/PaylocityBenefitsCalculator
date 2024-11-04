using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly BenefitsDbContext _context;

    private const decimal PaychecksPerYear = 26m;
    private const decimal MonthlyBenefitsBaseCost = 1000m;
    private const decimal MonthlyDependentBaseCost = 600m;
    private const decimal MonthlyElderDependentPremium = 200m;
    private const decimal ElderDependentThreshold = 50m;
    private const decimal HighIncomePercentagePremium = 0.02m;
    private const decimal HighIncomeThreshold = 80000m;

    public EmployeesController(BenefitsDbContext context) => _context = context;

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Dependents)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);

        if (employee is not null)
        {
            var dependents = employee.Dependents;
            var dependentsDto = new List<GetDependentDto>();

            var monthlyDependentCost = 0.0m;

            if (dependents.Any())
            {
                foreach (var dependent in dependents)
                {
                    int age = GetAge(dependent.DateOfBirth);
                    bool isElder = age > ElderDependentThreshold;
                    decimal additionalCost = isElder ? MonthlyElderDependentPremium : 0;
                    monthlyDependentCost += MonthlyDependentBaseCost + additionalCost;

                    dependentsDto.Add(new GetDependentDto
                    {
                        Id = dependent.DependentId,
                        FirstName = dependent.FirstName,
                        LastName = dependent.LastName,
                        Relationship = dependent.Relationship,
                        DateOfBirth = dependent.DateOfBirth
                    });
                }
            }

            var grossPaycheckAmount = employee.Salary / PaychecksPerYear;

            var monthlyBenefitsCost = MonthlyBenefitsBaseCost + monthlyDependentCost;

            var isHighIncome = employee.Salary > HighIncomeThreshold;
            var highIncomePremium = isHighIncome ? employee.Salary * HighIncomePercentagePremium : 0m;

            var yearlyBenefitsCost = monthlyBenefitsCost * 12m + highIncomePremium;

            var benefitCostPerPaycheck = yearlyBenefitsCost / PaychecksPerYear;

            var netPaycheckAmount = grossPaycheckAmount - benefitCostPerPaycheck;

            var employeeDto = new GetEmployeeDto
            {
                Id = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                PaycheckGrossAmount = grossPaycheckAmount,
                PaycheckBenefitsCost = benefitCostPerPaycheck,
                PaycheckNetAmount = netPaycheckAmount,
                DateOfBirth = employee.DateOfBirth,
                Dependents = dependentsDto
            };

            return new ApiResponse<GetEmployeeDto>
            {
                Data = employeeDto,
                Success = true
            };
        }

        return NotFound();
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        // update: using efcore, created dbContext to access localdb
        var employees = await _context.Employees.Include(e => e.Dependents).ToListAsync();

        var employeeDtos = new List<GetEmployeeDto>();

        foreach (var employee in employees)
        {
            var dependents = employee.Dependents;

            var dependentsDto = new List<GetDependentDto>();

            if (dependents.Any())
            {
                foreach (var dependent in dependents)
                {
                    dependentsDto.Add(new GetDependentDto
                    {
                        Id = dependent.DependentId,
                        FirstName = dependent.FirstName,
                        LastName = dependent.LastName,
                        Relationship = dependent.Relationship,
                        DateOfBirth = dependent.DateOfBirth
                    });
                }
            }

            employeeDtos.Add(new GetEmployeeDto
            {
                Id = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = dependentsDto
            });
        }

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employeeDtos,
            Success = true
        };

        return result;
    }

    private int GetAge(DateTime dob) => (int)Math.Floor((DateTime.Now - dob).TotalDays / 365.25D);
}
