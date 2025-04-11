using FluentValidation;
using HRM.Application.DTOs.Requests;

namespace HRM.Application.Validators
{
    public class ProjectRequestValidator : AbstractValidator<ProjectRequestDTO>
    {
        public ProjectRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name can not be empty.")
                .MaximumLength(100).WithMessage("Project name must not exceed 100 characters");
        }
    }
}
