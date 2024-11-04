using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Dependent
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // I want to control ID assignment to seed db
    public int DependentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
