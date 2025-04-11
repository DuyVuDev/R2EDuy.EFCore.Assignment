namespace HRM.Application.DTOs.Responses
{
    public class SalaryResponseDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
