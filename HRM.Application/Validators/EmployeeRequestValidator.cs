using FluentValidation;
using HRM.Application.DTOs.Requests;

namespace HRM.Application.Validators
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequestDTO>
    {
        public EmployeeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Employee name can not be empty.")
                .MaximumLength(100).WithMessage("Employee name must not exceed 100 characters");
            RuleFor(x => x.JoinedDate)
                .NotEmpty().WithMessage("Joined date can not be empty.")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Joined date must be in the past or present.");
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department ID can not be empty.");
        }
    }
}
