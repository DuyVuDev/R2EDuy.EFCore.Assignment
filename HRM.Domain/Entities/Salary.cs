namespace HRM.Domain.Entities
{
    public class Salary
    {
        public Guid Id { get; set; }
        public Employee? Employee { get; set; }
        public Guid EmployeeId { get; set; }
        public required decimal Amount { get; set; }
    }
}
