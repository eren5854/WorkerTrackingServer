using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Auth.RegisterAdmin;
public sealed class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
{
    public RegisterAdminCommandValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MinimumLength(3).MaximumLength(150).WithMessage("First Name must be between 3 and 150 characters");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MinimumLength(3).MaximumLength(250).WithMessage("Last Name must be between 3 and 250 characters");

        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).MaximumLength(250).WithMessage("Username must be between 3 and 250 characters");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .MinimumLength(10).MaximumLength(250).WithMessage("Email must be between 10 and 250 characters");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");
    }
}
