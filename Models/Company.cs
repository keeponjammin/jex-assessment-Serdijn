using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jex_assessment.Models
{
    public class Company
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address? Address { get; set; }

        public ICollection<Vacancy>? Vacancies { get; set; }
    }
}
