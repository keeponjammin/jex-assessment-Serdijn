using jex_assessment.Data;
using jex_assessment.Models;

namespace jex_assessment.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedDataAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AssessmentDbContext>();

            if (!context.Addresses.Any() && !context.Companies.Any() && !context.Vacancies.Any())
            {
                var addresses = new List<Address>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Street = "Keizersgracht 123",
                        City = "Amsterdam",
                        State = "Noord-Holland",
                        PostalCode = "1015 CJ",
                        Country = "Netherlands",
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Street = "Stationsplein 1",
                        City = "Rotterdam",
                        State = "Zuid-Holland",
                        PostalCode = "3013 AJ",
                        Country = "Netherlands",
                    },
                };

                var companies = new List<Company>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Van der Berg BV",
                        AddressId = addresses[0].Id,
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "De Jong Groep",
                        AddressId = addresses[1].Id,
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Van der Meer Organisatie",
                        AddressId = addresses[0].Id,
                    },
                };

                var vacancies = new List<Vacancy>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Softwareontwikkelaar",
                        Description = "Ontwikkel en onderhoud innovatieve softwareoplossingen.",
                        CompanyId = companies[0].Id,
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Projectmanager",
                        Description = "Beheer projecten en zorg voor een succesvolle uitvoering.",
                        CompanyId = companies[1].Id,
                    },
                };

                await context.Addresses.AddRangeAsync(addresses);
                await context.Companies.AddRangeAsync(companies);
                await context.Vacancies.AddRangeAsync(vacancies);

                await context.SaveChangesAsync();
            }
        }
    }
}
