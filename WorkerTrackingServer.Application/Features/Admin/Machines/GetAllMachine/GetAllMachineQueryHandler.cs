using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.GetAllMachine;
internal sealed class GetAllMachineQueryHandler(
    IMachineRepository machineRepository) : IRequestHandler<GetAllMachineQuery, Result<List<Machine>>>
{
    public async Task<Result<List<Machine>>> Handle(GetAllMachineQuery request, CancellationToken cancellationToken)
    {
        List<Machine> machines = await machineRepository.GetAll().OrderBy(o => o.CreatedDate).ToListAsync(cancellationToken);
        return Result<List<Machine>>.Succeed(machines);
    }
}
