using jex_assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace jex_assessment.Data
{
    public class AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
        : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Company-Address relationship
            modelBuilder.Entity<Company>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Companies)
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Company-Vacancy relationship
            modelBuilder
                .Entity<Vacancy>()
                .HasOne(v => v.Company)
                .WithMany(c => c.Vacancies)
                .HasForeignKey(v => v.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            modelBuilder.Entity<Company>().HasIndex(c => c.Name);
            modelBuilder.Entity<Company>().HasIndex(c => c.AddressId);
            modelBuilder.Entity<Vacancy>().HasIndex(v => v.Title);
            modelBuilder.Entity<Vacancy>().HasIndex(v => v.CompanyId);
            modelBuilder.Entity<Address>().HasIndex(a => new { a.Street, a.City, a.State, a.PostalCode, a.Country });
        }
    }
}
