using FluentValidation;
using HRM.Application.DTOs.Requests;

namespace HRM.Application.Validators
{
    public class DepartmentRequestValidator : AbstractValidator<DepartmentRequestDTO>
    {
        public DepartmentRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name can not be empty.")
                .MaximumLength(100).WithMessage("Department name must not exceed 100 characters");
        }
    }
}
