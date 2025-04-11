using HRM.Application.Repositories;
using HRM.Domain.Entities;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.RepositoryImplements
{
    public class ProjectEmployeeRepository : GenericRepository<ProjectEmployee>, IProjectEmployeeRepository
    {
        private readonly HRMDbContext _context;
        private readonly DbSet<ProjectEmployee> _dbSet;

        public ProjectEmployeeRepository(HRMDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<ProjectEmployee>();
        }

        public async Task<IEnumerable<ProjectEmployee>> GetByEmployeeIdAsync(Guid employeeId)
        {
            var projectEmployees = await _dbSet.AsNoTracking()
                .Where(pe => pe.EmployeeId == employeeId)
                .ToListAsync();
            return projectEmployees;
        }

        public async Task<IEnumerable<ProjectEmployee>> GetByProjectIdAsync(Guid projectId)
        {
            var projectEmployees = await _dbSet.AsNoTracking()
                .Where(pe => pe.ProjectId == projectId)
                .ToListAsync();
            return projectEmployees;
        }

        public async Task<ProjectEmployee?> GetByKeysAsync(Guid projectId, Guid employeeId)
        {
            var projectEmployee = await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
            return projectEmployee;
        }
    }
}
