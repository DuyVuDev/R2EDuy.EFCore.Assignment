using HRM.Application.Repositories;
using HRM.Domain.Entities;
using HRM.Persistance;

namespace HRM.Infrastructure.RepositoryImplements;

public class DepartmentRepository(HRMDbContext context) : GenericRepository<Department>(context), IDepartmentRepository
{
}
