using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
public sealed class UpdateWorkerCommandValidator : AbstractValidator<UpdateWorkerCommand>
{
    public UpdateWorkerCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Worker Id is required");

        RuleFor(r => r.FirstName)
           .NotEmpty().WithMessage("First Name is required")
           .MinimumLength(3).MaximumLength(150).WithMessage("First Name must be between 3 and 150 characters");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MinimumLength(3).MaximumLength(150).WithMessage("Last Name must be between 3 and 150 characters");

        RuleFor(r => r.UserName)
           .NotEmpty().WithMessage("User Name is required")
           .MinimumLength(3).MaximumLength(100).WithMessage("User Name must be between 3 and 100 characters");
    }
}
