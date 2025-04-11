using HRM.Application.Repositories;
using HRM.Domain.Entities;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.RepositoryImplements;

public class ProjectRepository(HRMDbContext context) : GenericRepository<Project>(context), IProjectRepository
{
}


