namespace HRM.Application.DTOs.Responses
{
    public class ProjectEmployeeResponseDTO
    {

        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
        public bool Enabled { get; set; }
    }
}
