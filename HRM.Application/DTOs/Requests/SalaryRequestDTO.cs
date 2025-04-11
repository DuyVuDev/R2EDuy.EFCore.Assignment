namespace HRM.Application.DTOs.Requests
{
    public class SalaryRequestDTO
    {
        public required decimal Amount { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
