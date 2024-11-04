using Api.Dtos.Dependent;

namespace Api.Services;

public interface IDependentService
{
    public GetDependentDto GetDependent(int id);
    public List<GetDependentDto> GetAllDependents();
}
