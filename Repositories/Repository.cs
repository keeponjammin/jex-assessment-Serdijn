using jex_assessment.Data;
using jex_assessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace jex_assessment.Repositories
{
    public class Repository<TEntity>(AssessmentDbContext context) : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly AssessmentDbContext Context = context;
        protected DbSet<TEntity> Entities => Context.Set<TEntity>();

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            Entities.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Entities.Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await Entities.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            Entities.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
