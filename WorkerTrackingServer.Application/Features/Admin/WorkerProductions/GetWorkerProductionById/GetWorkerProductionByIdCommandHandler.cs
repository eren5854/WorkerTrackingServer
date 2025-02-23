using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetWorkerProductionById;
internal sealed class GetWorkerProductionByIdCommandHandler(
    IWorkerProductionRepository workerProductionRepository) : IRequestHandler<GetWorkerProductionByIdCommand, Result<WorkerProduction>>
{
    public async Task<Result<WorkerProduction>> Handle(GetWorkerProductionByIdCommand request, CancellationToken cancellationToken)
    {
        WorkerProduction? workerProduction = await workerProductionRepository.Where(w => w.Id == request.Id).Include(i => i.AppUser).Include(i => i.Product).FirstOrDefaultAsync(cancellationToken);
        if (workerProduction is null)
        {
            return Result<WorkerProduction>.Failure("Worker production not found");
        }

        return Result<WorkerProduction>.Succeed(workerProduction);
    }
}
