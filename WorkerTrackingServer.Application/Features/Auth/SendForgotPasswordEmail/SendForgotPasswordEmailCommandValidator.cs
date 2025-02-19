using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Auth.SendForgotPasswordEmail;
public sealed class SendForgotPasswordEmailCommandValidator : AbstractValidator<SendForgotPasswordEmailCommand>
{
    public SendForgotPasswordEmailCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");
    }
}
