using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Auth.ForgotPassword;
public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(r => r.ForgotPasswordCode)
            .NotEmpty().WithMessage("Forgot password code is required")
            .InclusiveBetween(100000, 999999).WithMessage("Forgot password code must be 6 digits");
    }
}
