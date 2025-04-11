using FluentValidation;
using HRM.Application.DTOs.Requests;

namespace HRM.Application.Validators
{
    public class SalaryRequestValidator : AbstractValidator<SalaryRequestDTO>
    {
        public SalaryRequestValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Amount is required.")
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");
        }
    }
}
