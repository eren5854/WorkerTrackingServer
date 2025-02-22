using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetWorkerProductionById;
internal sealed class GetWorkerProductionByIdCommandHandler(
    IWorkerProductionRepository workerProductionRepository) : IRequestHandler<GetWorkerProductionByIdCommand, Result<WorkerProduction>>
{
    public async Task<Result<WorkerProduction>> Handle(GetWorkerProductionByIdCommand request, CancellationToken cancellationToken)
    {
        WorkerProduction workerProduction = await workerProductionRepository.GetByExpressionAsync(g => g.Id == request.Id,cancellationToken);
        if (workerProduction is null)
        {
            return Result<WorkerProduction>.Failure("Worker production not found");
        }

        return Result<WorkerProduction>.Succeed(workerProduction);
    }
}
