using HRM.Domain.Entities;

namespace HRM.Application.Repositories
{
    public interface ISalaryRepository : IGenericRepository<Salary>
    {
        Task<Salary?> GetByEmployeeIdAsync(Guid employeeId);
    }
}
