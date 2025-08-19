namespace jex_assessment.DTOs
{
    public class CompanyDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid AddressId { get; set; }
        public List<VacancyDto>? Vacancies { get; set; }
    }
}
