using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                PaycheckGrossAmount = 75420.99m / 26m,
                PaycheckBenefitsCost = 1000m * 12m / 26m,
                PaycheckNetAmount = (75420.99m / 26m) - (1000m * 12m / 26m),
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                PaycheckGrossAmount = 92365.22m / 26m,
                PaycheckBenefitsCost = ((3m * 600m * 12m) + (1000m * 12m) + (92365.22m * 0.02m)) / 26,
                PaycheckNetAmount = (92365.22m / 26m) - (((3m * 600m * 12m) + (1000m * 12m) + (92365.22m * 0.02m)) / 26),
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                PaycheckGrossAmount = 143211.12m / 26m,
                PaycheckBenefitsCost = ((600m * 12m) + (1000m * 12m) + (143211.12m * 0.02m)) / 26,
                PaycheckNetAmount = (143211.12m / 26m) - (((600m * 12m) + (1000m * 12m) + (143211.12m * 0.02m)) / 26m),
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            PaycheckGrossAmount = 75420.99m / 26m,
            PaycheckBenefitsCost = 1000m * 12m / 26m,
            PaycheckNetAmount = (75420.99m / 26m) - (1000m * 12m / 26m),
            DateOfBirth = new DateTime(1984, 12, 30)
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAHighIncomeEmployee_ShouldReturnCorrectEmployeeWithCorrectPaycheckCalculations()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/3");
        var salary = 143211.12m;
        var paycheckGrossAmount = salary / 26m;
        var empAnnualBenefitsCost = (1000m * 12m) + (salary * 0.02m);
        var depAnnualBenefitsCost = 600m * 12m;
        var paycheckBenefitsCost = (empAnnualBenefitsCost + depAnnualBenefitsCost) / 26m;
        var employee = new GetEmployeeDto
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = salary,
            PaycheckGrossAmount = paycheckGrossAmount,
            PaycheckBenefitsCost = paycheckBenefitsCost,
            PaycheckNetAmount = paycheckGrossAmount - paycheckBenefitsCost,
            DateOfBirth = new DateTime(1963, 2, 17),
            Dependents = new List<GetDependentDto>
            {
                new()
                {
                    Id = 4,
                    FirstName = "DP",
                    LastName = "Jordan",
                    Relationship = Relationship.DomesticPartner,
                    DateOfBirth = new DateTime(1974, 1, 2)
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}
