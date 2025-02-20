using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.GetMachineById;
internal sealed class GetMachineByIdCommandHandler(
    IMachineRepository machineRepository) : IRequestHandler<GetMachineByIdCommand, Result<Machine>>
{
    public async Task<Result<Machine>> Handle(GetMachineByIdCommand request, CancellationToken cancellationToken)
    {
        Machine machine = await machineRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (machine is null)
        {
            return Result<Machine>.Failure("Machine not found");
        }

        return Result<Machine>.Succeed(machine);
    }
}
