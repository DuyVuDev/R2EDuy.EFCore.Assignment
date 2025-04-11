using HRM.Domain.Entities;

namespace HRM.Application.Repositories
{
    public interface IProjectEmployeeRepository : IGenericRepository<ProjectEmployee>
    {
        Task<IEnumerable<ProjectEmployee>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<ProjectEmployee>> GetByEmployeeIdAsync(Guid employeeId);
        Task<ProjectEmployee?> GetByKeysAsync(Guid projectId, Guid employeeId);
    }
}
