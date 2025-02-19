using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Auth.ChangePasswordUsingToken;
public sealed class ChangePasswordUsingTokenCommandValidator : AbstractValidator<ChangePasswordUsingTokenCommand>
{
    public ChangePasswordUsingTokenCommandValidator()
    {
        RuleFor(r => r.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");
        //.Matches(@"[\!\?\*\.\@\#]").WithMessage("Password must contain at least one special character (!?*.@#).");
    }
}
