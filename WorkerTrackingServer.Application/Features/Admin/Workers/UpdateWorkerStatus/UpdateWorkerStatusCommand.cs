using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorkerStatus;
public sealed record UpdateWorkerStatusCommand(Guid Id) : IRequest<Result<string>>;
