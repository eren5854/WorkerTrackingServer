using ED.Result;

namespace WorkerTrackingServer.Application.Services;
public interface IProductionCalculateService
{
    public Task<Result<string>> WeeklyProductionCalculate(CancellationToken cancellationToken);
    public Task<Result<string>> WeeklyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> MonthlyProductionCalculate(CancellationToken cancellationToken);
    public Task<Result<string>> MonthlyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken);
    public Task<Result<string>> YearlyProductionCalculate(CancellationToken cancellationToken);
    public Task<Result<string>> YearlyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken);

    public Task<Result<string>> ProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken);
}
