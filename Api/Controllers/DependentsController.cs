using Api.Dtos.Dependent;
using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly BenefitsDbContext _context;

    public DependentsController(BenefitsDbContext context) => _context = context;

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _context.Dependents.FirstOrDefaultAsync(x => x.DependentId == id);

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
        var dependents = await _context.Dependents.ToListAsync();

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
