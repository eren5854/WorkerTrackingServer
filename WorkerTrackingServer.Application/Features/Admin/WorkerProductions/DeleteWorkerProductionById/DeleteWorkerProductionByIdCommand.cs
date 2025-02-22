using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.DeleteWorkerProductionById;
public sealed record DeleteWorkerProductionByIdCommand(Guid Id) : IRequest<Result<string>>;
