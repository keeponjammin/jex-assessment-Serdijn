using System.Linq;
using jex_assessment.DTOs;
using jex_assessment.Models;

namespace jex_assessment.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyDto ToDto(Company company) =>
            new()
            {
                Id = company.Id,
                Name = company.Name,
                AddressId = company.AddressId,
                Vacancies = (company.Vacancies ?? []).Select(VacancyMapper.ToDto).ToList(),
            };

        public static Company ToEntity(CompanyDto dto) =>
            new()
            {
                Id = dto.Id,
                Name = dto.Name,
                AddressId = dto.AddressId,
            };
    }
}
