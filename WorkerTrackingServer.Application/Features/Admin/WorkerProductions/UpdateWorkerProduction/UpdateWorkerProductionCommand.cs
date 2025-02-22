using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.UpdateWorkerProduction;
public sealed record UpdateWorkerProductionCommand(
    Guid Id,
    Guid AppUserId,
    Guid ProductId,
    int? DailyTarget,
    int? WeeklyTarget,
    int? MonthlyTarget,
    int? YearlyTarget) : IRequest<Result<string>>;
