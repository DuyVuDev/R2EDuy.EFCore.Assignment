using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Application.Repositories;
using HRM.Domain.Entities;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.RepositoryImplements;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private new readonly HRMDbContext _context;

    public EmployeeRepository(HRMDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeWithDepartmentDTO>> GetAllWithDepartmentAsync()
    {
        return await _context.Employees
            .Join(
                _context.Departments,
                e => e.DepartmentId,
                d => d.Id,
                (e, d) => new EmployeeWithDepartmentDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    JoinedDate = e.JoinedDate,
                    DepartmentId = d.Id,
                    DepartmentName = d.Name
                }
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<EmployeeWithProjectsDTO>> GetAllWithProjectsAsync()
    {
        return await _context.Employees
            .GroupJoin(
                _context.ProjectEmployees,
                e => e.Id,
                pe => pe.EmployeeId,
                (e, pes) => new EmployeeWithProjectsDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId,
                    JoinedDate = e.JoinedDate,
                    projectIds = pes.Select(pe => pe.ProjectId).ToList()
                }
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<EmployeeWithSalaryDTO>> FilterEmployeeAsync(EmployeeFilterDTO filter)
    {
        var query = _context.Employees
        .Join(_context.Salaries,
              e => e.Id,
              s => s.EmployeeId,
              (e, s) => new { e, s })
        .AsQueryable();

        if (filter.Amount.HasValue && filter.SalaryFilterChoice.HasValue)
        {
            query = filter.SalaryFilterChoice.Value switch
            {
                FilterChoice.GreaterThan => query.Where(x => x.s.Amount > filter.Amount.Value),
                FilterChoice.LessThan => query.Where(x => x.s.Amount < filter.Amount.Value),
                FilterChoice.EqualTo => query.Where(x => x.s.Amount == filter.Amount.Value),
                FilterChoice.GreaterThanOrEqualTo => query.Where(x => x.s.Amount >= filter.Amount.Value),
                FilterChoice.LessThanOrEqualTo => query.Where(x => x.s.Amount <= filter.Amount.Value),
                _ => query
            };
        }

        // Lọc theo ngày gia nhập nếu có
        if (filter.JoinedDate.HasValue && filter.JoinedDateFilterChoice.HasValue)
        {
            var joinedDate = filter.JoinedDate.Value;

            query = filter.JoinedDateFilterChoice.Value switch
            {
                FilterChoice.GreaterThan => query.Where(x => x.e.JoinedDate > joinedDate),
                FilterChoice.LessThan => query.Where(x => x.e.JoinedDate < joinedDate),
                FilterChoice.EqualTo => query.Where(x => x.e.JoinedDate == joinedDate),
                FilterChoice.GreaterThanOrEqualTo => query.Where(x => x.e.JoinedDate >= joinedDate),
                FilterChoice.LessThanOrEqualTo => query.Where(x => x.e.JoinedDate <= joinedDate),
                _ => query
            };
        }

        return await query
            .Select(x => new EmployeeWithSalaryDTO
            {
                Id = x.e.Id,
                Name = x.e.Name,
                JoinedDate = x.e.JoinedDate,
                DepartmentId = x.e.DepartmentId,
                SalaryAmount = x.s.Amount
            })
            .ToListAsync();
    }
}
