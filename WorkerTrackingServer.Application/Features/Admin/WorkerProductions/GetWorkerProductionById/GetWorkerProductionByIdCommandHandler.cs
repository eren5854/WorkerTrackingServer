using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Application.Services;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetWorkerProductionById;
internal sealed class GetWorkerProductionByIdCommandHandler(
    IWorkerProductionRepository workerProductionRepository,
    IProductionCalculateService productionCalculateService) : IRequestHandler<GetWorkerProductionByIdCommand, Result<WorkerProduction>>
{
    public async Task<Result<WorkerProduction>> Handle(GetWorkerProductionByIdCommand request, CancellationToken cancellationToken)
    {
        await productionCalculateService.ProductionCalculateByWorkerProductionId(request.Id, cancellationToken);

        WorkerProduction? workerProduction = await workerProductionRepository
            .Where(w => w.Id == request.Id)
            .Include(i => i.AppUser)
            .Include(i => i.Product)
            .Include(i => i.DailyProductions!.OrderByDescending(o => o.CreatedDate))
            .Include(i => i.WeeklyProductions!.OrderByDescending(o => o.CreatedDate))
            .Include(i => i.MonthlyProductions!.OrderByDescending(o => o.CreatedDate))
            .Include(i => i.YearlyProductions!.OrderByDescending(o => o.CreatedDate))
            .FirstOrDefaultAsync(cancellationToken);
        if (workerProduction is null)
        {
            return Result<WorkerProduction>.Failure("Worker production not found");
        }

        //await productionCalculateService.WeeklyProductionCalculateByWorkerProductionId(workerProduction.Id, cancellationToken);
        //await productionCalculateService.MonthlyProductionCalculateByWorkerProductionId(workerProduction.Id, cancellationToken);


        return Result<WorkerProduction>.Succeed(workerProduction);
    }
}
