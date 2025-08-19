using jex_assessment.Managers.Interfaces;
using jex_assessment.Models;
using jex_assessment.Repositories.Interfaces;

namespace jex_assessment.Managers
{
    public class VacancyManager(IVacancyRepository repository) : IVacancyManager
    {
        public async Task<List<Vacancy>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Vacancy?> GetByIdAsync(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<Vacancy> AddAsync(Vacancy vacancy)
        {
            await repository.AddAsync(vacancy);
            return vacancy;
        }

        public async Task<Vacancy?> UpdateAsync(Vacancy vacancy)
        {
            var existing = await repository.GetByIdAsync(vacancy.Id);
            if (existing == null)
                return null;
            await repository.UpdateAsync(vacancy);
            return vacancy;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
