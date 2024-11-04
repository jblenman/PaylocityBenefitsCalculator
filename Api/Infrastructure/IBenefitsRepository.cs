using Api.Models;

namespace Api.Infrastructure;

public interface IBenefitsRepository
{
    public Task<Employee> GetEmployeeByIdAsync(int id);
    public Task<List<Employee>> GetAllEmployeesAsync();
    public Task<Dependent> GetDependentByIdAsync(int id);
    public Task<List<Dependent>> GetAllDependentsAsync();
}
