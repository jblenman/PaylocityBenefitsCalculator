using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;

public class BenefitsRepository : IBenefitsRepository
{
    private readonly BenefitsDbContext _context;

    public BenefitsRepository(BenefitsDbContext context) => _context = context;

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Dependents)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.Include(e => e.Dependents).ToListAsync();
    }

    public async Task<Dependent> GetDependentByIdAsync(int id)
    {
        return await _context.Dependents.FirstOrDefaultAsync(x => x.DependentId == id);
    }

    public async Task<List<Dependent>> GetAllDependentsAsync()
    {
        return await _context.Dependents.ToListAsync();
    }
}
