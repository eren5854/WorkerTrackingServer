using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.WorkerRegister;
public sealed class WorkerRegisterCommandValidator : AbstractValidator<WorkerRegisterCommand>
{
    public WorkerRegisterCommandValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MinimumLength(3).MaximumLength(150).WithMessage("First Name must be between 3 and 150 characters");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MinimumLength(3).MaximumLength(150).WithMessage("Last Name must be between 3 and 150 characters");

        RuleFor(r => r.UserName)
           .NotEmpty().WithMessage("User Name is required")
           .MinimumLength(3).MaximumLength(100).WithMessage("User Name must be between 3 and 100 characters");

        RuleFor(r => r.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        //.Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
        //.Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
        //.Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");
    }
}
