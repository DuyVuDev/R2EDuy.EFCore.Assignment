namespace HRM.Domain.Entities
{
    public class Salary
    {
        public Guid Id { get; set; }
        public required Employee Employee { get; set; }
        public required decimal Amount { get; set; }
    }
}
