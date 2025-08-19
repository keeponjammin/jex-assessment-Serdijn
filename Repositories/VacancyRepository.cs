using jex_assessment.Data;
using jex_assessment.Models;
using jex_assessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace jex_assessment.Repositories
{
    public class VacancyRepository(AssessmentDbContext context)
        : Repository<Vacancy>(context),
            IVacancyRepository
    {
        public override async Task<List<Vacancy>> GetAllAsync()
        {
            return await Context.Vacancies.Include(v => v.Company).ToListAsync();
        }

        public override async Task<Vacancy?> GetByIdAsync(Guid id)
        {
            return await Context
                .Vacancies.Include(v => v.Company)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Add or override other methods if you need custom logic
    }
}
