using Api.Dtos.Employee;
using Api.Infrastructure;

namespace Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IBenefitsRepository _repository;

    public EmployeeService(IBenefitsRepository repository) => _repository = repository;

    List<GetEmployeeDto> IEmployeeService.GetAllEmployees()
    {
        throw new NotImplementedException();
    }

    GetEmployeeDto IEmployeeService.GetEmployee(int id)
    {
        throw new NotImplementedException();
    }
}
