using HRM.Application.Repositories;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.RepositoryImplements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly HRMDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(HRMDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await Task.CompletedTask;
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }

}
