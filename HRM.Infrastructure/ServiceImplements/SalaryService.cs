using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;
using HRM.Application.Repositories;
using HRM.Application.Services;

namespace HRM.Infrastructure.ServiceImplements
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository;
        public SalaryService(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<IEnumerable<SalaryResponseDTO>> GetAllSalarysAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            return salaries.Select(s => new SalaryResponseDTO
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                Amount = s.Amount
            });
        }

        public async Task<SalaryResponseDTO> GetSalaryByIdAsync(Guid id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            if (salary == null)
            {
                throw new KeyNotFoundException($"Salary with ID {id} not found.");
            }
            return new SalaryResponseDTO
            {
                Id = salary.Id,
                EmployeeId = salary.EmployeeId,
                Amount = salary.Amount
            };
        }

        public async Task<bool> UpdateSalaryAsync(Guid id, SalaryRequestDTO salaryRequest)
        {
            var existingSalary = await _salaryRepository.GetByIdAsync(id);
            if (existingSalary == null)
            {
                throw new KeyNotFoundException($"Salary with ID {id} not found.");
            }
            existingSalary.Amount = salaryRequest.Amount;

            await _salaryRepository.UpdateAsync(existingSalary);
            await _salaryRepository.SaveChangesAsync();

            return true;
        }
    }
}
