using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorkerCode;
public sealed record UpdateWorkerCodeCommand(Guid Id) : IRequest<Result<string>>;
