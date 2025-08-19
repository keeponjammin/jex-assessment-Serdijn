using jex_assessment.DTOs;
using jex_assessment.Models;

namespace jex_assessment.Mappers
{
    public static class VacancyMapper
    {
        public static VacancyDto ToDto(Vacancy vacancy) =>
            new()
            {
                Id = vacancy.Id,
                Title = vacancy.Title,
                Description = vacancy.Description,
                CompanyId = vacancy.CompanyId,
            };

        public static Vacancy ToEntity(VacancyDto dto) =>
            new()
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CompanyId = dto.CompanyId,
            };
    }
}
