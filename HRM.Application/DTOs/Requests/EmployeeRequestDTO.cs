namespace HRM.Application.DTOs.Requests
{
    public class EmployeeRequestDTO
    {
        public required string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }
        public decimal Amount { get; set; }
        public IList<Guid>? ProjectIds { get; set; }
    }
}
