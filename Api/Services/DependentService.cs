using Api.Dtos.Dependent;
using Api.Infrastructure;

namespace Api.Services;

public class DependentService : IDependentService
{
    private readonly IBenefitsRepository _repository;

    public DependentService(IBenefitsRepository repository) => _repository = repository;

    List<GetDependentDto> IDependentService.GetAllDependents()
    {
        throw new NotImplementedException();
    }

    GetDependentDto IDependentService.GetDependent(int id)
    {
        throw new NotImplementedException();
    }
}
