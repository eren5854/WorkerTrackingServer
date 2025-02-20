using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachineStatus;
internal sealed class UpdateMachineStatusCommandHandler(
    IMachineRepository machineRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateMachineStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMachineStatusCommand request, CancellationToken cancellationToken)
    {
        Machine machine = await machineRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (machine is null)
        {
            return Result<string>.Failure("Machine not found");
        }

        machine.IsActive = !machine.IsActive;

        machineRepository.Update(machine);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Machine Status updated successfully");
    }
}
