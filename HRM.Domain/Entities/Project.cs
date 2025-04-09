namespace HRM.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public IList<ProjectEmployee> ProjectEmployees { get; set; }

    }
}
