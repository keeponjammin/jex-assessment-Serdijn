using jex_assessment.DTOs;
using jex_assessment.Managers.Interfaces;
using jex_assessment.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace jex_assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController(
        IVacancyManager vacancyManager,
        ICompanyManager companyManager,
        ILogger<VacanciesController> logger
    ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<VacancyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok<List<VacancyDto>>, StatusCodeHttpResult>> GetAllAsync()
        {
            try
            {
                var vacancies = await vacancyManager.GetAllAsync();
                return TypedResults.Ok(vacancies.Select(VacancyMapper.ToDto).ToList());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in GetAllAsync");
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok<VacancyDto>, NotFound, StatusCodeHttpResult>> GetByIdAsync(
            Guid id
        )
        {
            try
            {
                var vacancy = await vacancyManager.GetByIdAsync(id);
                if (vacancy == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(VacancyMapper.ToDto(vacancy));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while getting vacancy with id {id}", id);
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok, BadRequest, StatusCodeHttpResult>> CreateAsync(
            CreateVacancyDto dto
        )
        {
            try
            {
                var company = await companyManager.GetByIdAsync(dto.CompanyId);
                if (company == null)
                {
                    return TypedResults.BadRequest();
                }
                var vacancy = new Models.Vacancy
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Description = dto.Description,
                    CompanyId = dto.CompanyId,
                };
                await vacancyManager.AddAsync(vacancy);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when creating vacancy");
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
            VacancyDto dto
        )
        {
            try
            {
                if (id != dto.Id)
                {
                    return TypedResults.BadRequest();
                }
                var vacancy = VacancyMapper.ToEntity(dto);
                var updated = await vacancyManager.UpdateAsync(vacancy);
                if (updated == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when updating vacancy with id {id}", id);
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
                var deleted = await vacancyManager.DeleteAsync(id);
                if (!deleted)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred when deleting a vacancy with id {id}", id);
                return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
