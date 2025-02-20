using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.CreateMachine;
public sealed class CreateMachineCommandValidator : AbstractValidator<CreateMachineCommand>
{
    public CreateMachineCommandValidator()
    {
        RuleFor(r => r.MachineName)
            .MaximumLength(200).WithMessage("Machine name max 200 character.");
        RuleFor(r => r.MachineBrand)
            .MaximumLength(200).WithMessage("Machine brand max 200 character.");
        RuleFor(r => r.MachineModel)
            .MaximumLength(200).WithMessage("Machine model max 200 character.");
        RuleFor(r => r.MachineLocation)
            .MaximumLength(200).WithMessage("Machine location max 200 character.");
        RuleFor(r => r.MachineDescription)
            .MaximumLength(3000).WithMessage("Machine name max 3000 character.");
    }
}
