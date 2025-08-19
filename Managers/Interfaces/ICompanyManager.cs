using jex_assessment.Models;

namespace jex_assessment.Managers.Interfaces
{
    public interface ICompanyManager
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(Guid id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task<bool> DeleteAsync(Guid id);
    }
}
