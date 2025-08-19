using jex_assessment.Data;
using jex_assessment.Models;
using jex_assessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace jex_assessment.Repositories
{
    public class CompanyRepository(AssessmentDbContext context)
        : Repository<Company>(context),
            ICompanyRepository
    {
        public override async Task<List<Company>> GetAllAsync()
        {
            return await Context.Companies.Include(c => c.Vacancies).ToListAsync();
        }

        public override async Task<Company?> GetByIdAsync(Guid id)
        {
            return await Context
                .Companies.Include(c => c.Vacancies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Add or override other methods if you need custom logic
    }
}
