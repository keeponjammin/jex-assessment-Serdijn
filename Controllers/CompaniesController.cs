using jex_assessment.DTOs;
using jex_assessment.Managers.Interfaces;
using jex_assessment.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace jex_assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController(ICompanyManager manager, ILogger<CompaniesController> logger)
        : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok<List<CompanyDto>>, StatusCodeHttpResult>> GetAllAsync()
        {
            try
            {
                var companies = await manager.GetAllAsync();
                return TypedResults.Ok(companies.Select(CompanyMapper.ToDto).ToList());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when getting all companies.");
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok<CompanyDto>, NotFound, StatusCodeHttpResult>> GetByIdAsync(
            Guid id
        )
        {
            try
            {
                var company = await manager.GetByIdAsync(id);
                if (company == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(CompanyMapper.ToDto(company));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while getting company with id {id}", id);
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok, BadRequest, StatusCodeHttpResult>> CreateAsync(CompanyDto dto)
        {
            try
            {
                var company = CompanyMapper.ToEntity(dto);
                await manager.AddAsync(company);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when creating company");
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok, NotFound, BadRequest, StatusCodeHttpResult>> UpdateAsync(
            Guid id,
            CompanyDto dto
        )
        {
            try
            {
                if (id != dto.Id)
                {
                    return TypedResults.BadRequest();
                }
                var company = CompanyMapper.ToEntity(dto);
                var existing = await manager.GetByIdAsync(id);
                if (existing == null)
                {
                    return TypedResults.NotFound();
                }
                await manager.UpdateAsync(company);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when updating company with id {id}", id);
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok, NotFound, StatusCodeHttpResult>> DeleteAsync(Guid id)
        {
            try
            {
                var deleted = await manager.DeleteAsync(id);
                if (!deleted)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when deleting a company with id {id}", id);
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
