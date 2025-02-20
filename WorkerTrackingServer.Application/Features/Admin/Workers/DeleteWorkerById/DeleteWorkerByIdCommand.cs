using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.DeleteWorkerById;
public sealed record DeleteWorkerByIdCommand(
    Guid Id) : IRequest<Result<string>>;
