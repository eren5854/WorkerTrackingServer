using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.CreateDepartment;
public sealed class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(r => r.DepartmentName)
            .NotEmpty().WithMessage("Department is required")
            .MaximumLength(150).WithMessage("Department name length can be maximum 150 characters.");

        RuleFor(r => r.DepartmentName)
            .MaximumLength(150).WithMessage("Department name length can be maximum 150 characters.");
    }
}
