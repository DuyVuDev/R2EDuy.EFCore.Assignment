using HRM.Application.Repositories;
using HRM.Domain.Entities;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.RepositoryImplements;
public class SalaryRepository(HRMDbContext context) : GenericRepository<Salary>(context), ISalaryRepository
{
    public async Task<Salary?> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(s => s.EmployeeId == employeeId);
    }
}

