namespace HRM.Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(object id);
        public Task<bool> AddAsync(TEntity entity);
        public Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
        public Task<bool> UpdateAsync(TEntity entity);
        public Task<bool> DeleteAsync(TEntity entity);
        public Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
        public Task SaveChangesAsync();
    }
}
