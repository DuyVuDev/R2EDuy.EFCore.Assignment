using HRM.Application.DTOs.Requests;
using HRM.Application.DTOs.Responses;

namespace HRM.Application.Services
{
    public interface ISalaryService
    {
        Task<SalaryResponseDTO> GetSalaryByIdAsync(Guid id);
        Task<IEnumerable<SalaryResponseDTO>> GetAllSalarysAsync();
        Task<bool> UpdateSalaryAsync(Guid id, SalaryRequestDTO salaryRequest);

    }
}
