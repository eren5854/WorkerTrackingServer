using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetAllWorkerProductionByAppUserId;
internal sealed class GetAllWorkerProductionByAppUserIdCommandHandler(
    IWorkerProductionRepository workerProductionRepository) : IRequestHandler<GetAllWorkerProductionByAppUserIdCommand, Result<List<WorkerProduction>>>
{
    public async Task<Result<List<WorkerProduction>>> Handle(GetAllWorkerProductionByAppUserIdCommand request, CancellationToken cancellationToken)
    {
        List<WorkerProduction> workerProductions = await workerProductionRepository.GetAll().Where(w => w.AppUserId == request.AppUserId).Include(i => i.AppUser).Include(i => i.Product).ToListAsync(cancellationToken);

        return Result<List<WorkerProduction>>.Succeed(workerProductions);
    }
}
