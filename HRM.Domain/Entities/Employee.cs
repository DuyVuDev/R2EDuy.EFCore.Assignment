namespace HRM.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required DateOnly JoinedDate { get; set; }
        public required Department Department { get; set; }
        public Salary Salary { get; set; }
        public Guid SalaryId { get; set; }
        public Guid DepartmentId { get; set; }
        public IList<ProjectEmployee> ProjectEmployees { get; set; }

    }
}
