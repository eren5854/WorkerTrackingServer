using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.CreateDepartmentProduction;
public sealed record CreateDepartmentProductionCommand(
    int DailyTarget,
    int? WeeklyTarget,
    int? MonthlyTarget,
    int? YearlyTarget,
    Guid ProductId,
    Guid DepartmentId): IRequest<Result<string>>;