using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.DeleteMachineById;
internal sealed class DeleteMachineByIdCommandHandler(
    IMachineRepository machineRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteMachineByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteMachineByIdCommand request, CancellationToken cancellationToken)
    {
        Machine machine = await machineRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (machine == null)
        {
            return Result<string>.Failure("Machine not found");
        }

        machine.IsDeleted = true;

        machineRepository.Update(machine);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Machine deleted successfully");
    }
}
