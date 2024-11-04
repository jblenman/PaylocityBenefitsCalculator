using Api.Models;

namespace Api.Infrastructure;

public static class BenefitsDbContextInitializer
{
    public static void Initialize(BenefitsDbContext context)
    {
        context.Database.EnsureDeleted();  // remove old
        context.Database.EnsureCreated();  // create new

        var employees = new List<Employee>
        {
            new()
            {
                EmployeeId = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                EmployeeId = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10)
            },
            new()
            {
                EmployeeId = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17)
            }
        };

        foreach (var employee in employees)
        {
            context.Employees.Add(employee);
        }

        context.SaveChanges();

        var dependents = new List<Dependent>
        {
            new()
            {
                DependentId = 1,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3),
                EmployeeId = 2
            },
            new()
            {
                DependentId = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23),
                EmployeeId = 2
            },
            new()
            {
                DependentId = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18),
                EmployeeId = 2
            },
            new()
            {
                DependentId = 4,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2),
                EmployeeId = 3
            }
        };

        foreach (var dependent in dependents)
        {
            context.Dependents.Add(dependent);
        }

        context.SaveChanges();
    }
}
