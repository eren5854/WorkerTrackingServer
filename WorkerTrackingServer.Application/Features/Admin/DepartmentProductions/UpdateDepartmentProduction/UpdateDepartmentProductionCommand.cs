using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.UpdateDepartmentProduction;
public sealed record UpdateDepartmentProductionCommand(
    Guid Id,
    int DailyTarget,
    int? WeeklyTarget,
    int? MonthlyTarget,
    int? YearlyTarget,
    Guid ProductId,
    Guid DepartmentId) : IRequest<Result<string>>;
