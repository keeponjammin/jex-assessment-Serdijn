using jex_assessment.Models;

namespace jex_assessment.Managers.Interfaces
{
    public interface IVacancyManager
    {
        Task<List<Vacancy>> GetAllAsync();
        Task<Vacancy?> GetByIdAsync(Guid id);
        Task<Vacancy> AddAsync(Vacancy vacancy);
        Task<Vacancy?> UpdateAsync(Vacancy vacancy);
        Task<bool> DeleteAsync(Guid id);
    }
}
