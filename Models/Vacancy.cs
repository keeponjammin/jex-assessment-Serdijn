using System.ComponentModel.DataAnnotations;

namespace jex_assessment.Models
{
    public class Vacancy
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }

        [Required]
        public required Guid CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
