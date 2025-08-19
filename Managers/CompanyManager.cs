using jex_assessment.Managers.Interfaces;
using jex_assessment.Models;
using jex_assessment.Repositories.Interfaces;

namespace jex_assessment.Managers
{
    public class CompanyManager(ICompanyRepository repository) : ICompanyManager
    {
        public async Task<List<Company>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Company company)
        {
            await repository.AddAsync(company);
        }

        public async Task UpdateAsync(Company company)
        {
            await repository.UpdateAsync(company);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
