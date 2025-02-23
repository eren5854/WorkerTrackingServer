using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetWorkerProductionById;
public sealed record GetWorkerProductionByIdCommand(Guid Id) : IRequest<Result<WorkerProduction>>;
