namespace HRM.Application.DTOs.Requests
{
    public enum FilterChoice
    {
        GreaterThan,
        LessThan,
        EqualTo,
        GreaterThanOrEqualTo,
        LessThanOrEqualTo
    }

    public class EmployeeFilterDTO
    {
        public decimal? Amount { get; set; }
        public FilterChoice? SalaryFilterChoice { get; set; }
        public DateOnly? JoinedDate { get; set; }
        public FilterChoice? JoinedDateFilterChoice { get; set; }
    }
}
