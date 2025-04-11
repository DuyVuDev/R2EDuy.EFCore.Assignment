using FluentValidation;
using HRM.Application.DTOs.Requests;

namespace HRM.Application.Validators
{
    public class EmployeeFilterValidator : AbstractValidator<EmployeeFilterDTO>
    {
        public EmployeeFilterValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage(errorMessage: "Amount must be greater than 0.");
            RuleFor(x => x.JoinedDate)
                .NotEmpty().WithMessage("Joined date can not be empty.")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Joined date must be in the past or present.");
            RuleFor(x => x.SalaryFilterChoice)
                .IsInEnum().WithMessage("Salary filter choice must be a valid enum value.");
            RuleFor(x => x.JoinedDateFilterChoice)
                .IsInEnum().WithMessage("Joined date filter choice must be a valid enum value.");


        }
    }

}
