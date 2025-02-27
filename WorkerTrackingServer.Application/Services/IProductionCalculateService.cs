using ED.Result;

namespace WorkerTrackingServer.Application.Services;
public interface IProductionCalculateService
{
    public Task<Result<string>> WeeklyProductionCalculate(CancellationToken cancellationToken);
    public Task<Result<string>> WeeklyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken);
}
