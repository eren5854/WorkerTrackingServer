using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.UpdateDepartment;
public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Department Id is required");

        RuleFor(r => r.DepartmentName)
           .NotEmpty().WithMessage("Department is required")
           .MaximumLength(150).WithMessage("Department name length can be maximum 150 characters.");

        RuleFor(r => r.DepartmentName)
            .MaximumLength(150).WithMessage("Department name length can be maximum 150 characters.");
    }
}
