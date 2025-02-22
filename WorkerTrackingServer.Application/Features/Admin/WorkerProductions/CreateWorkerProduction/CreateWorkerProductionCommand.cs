using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.CreateWorkerProduction;
public sealed record CreateWorkerProductionCommand(
    Guid AppUserId,
    Guid ProductId,
    int? DailyTarget,
    int? WeeklyTarget,
    int? MonthlyTarget,
    int? YearlyTarget) : IRequest<Result<string>>;
