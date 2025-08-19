using System.ComponentModel.DataAnnotations;

namespace jex_assessment.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Street { get; set; }

        [Required]
        [MaxLength(100)]
        public required string City { get; set; }

        [Required]
        [MaxLength(100)]
        public required string State { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PostalCode { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Country { get; set; }

        public ICollection<Company>? Companies { get; set; }
    }
}
