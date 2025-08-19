namespace jex_assessment.DTOs
{
    public class CreateVacancyDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Guid CompanyId { get; set; }
    }
}
