using Api.Dtos.Dependent;
using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IBenefitsRepository _repository;

    public DependentsController(IBenefitsRepository repository) => _repository = repository;

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _repository.GetDependentByIdAsync(id);

        if (dependent is not null)
        {
            return new ApiResponse<GetDependentDto>
            {
                Data = new GetDependentDto
                {
                    Id = dependent.DependentId,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    Relationship = dependent.Relationship,
                    DateOfBirth = dependent.DateOfBirth
                },
                Success = true
            };
        }

        return NotFound();
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _repository.GetAllDependentsAsync();

        var dependentsDto = new List<GetDependentDto>();

        foreach (var dependent in dependents)
        {
            dependentsDto.Add(new GetDependentDto
            {
                Id = dependent.DependentId,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                DateOfBirth = dependent.DateOfBirth
            });
        }

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependentsDto,
            Success = true
        };

        return result;
    }
}
