using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // I want to control ID assignment to seed db
    public int EmployeeId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
}
