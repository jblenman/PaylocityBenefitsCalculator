using Api.Dtos.Employee;

namespace Api.Services;

public interface IEmployeeService
{
    public GetEmployeeDto GetEmployee(int id);
    public List<GetEmployeeDto> GetAllEmployees();
}
