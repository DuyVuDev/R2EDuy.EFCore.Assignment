namespace HRM.Domain.Entities
{
    public class ProjectEmployee
    {

        public Project? Project { get; set; }
        public Employee? Employee { get; set; }
        public bool Enabled { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
