namespace HRM.Application.DTOs.Responses
{
    public class EmployeeResponseDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }
    }
}
