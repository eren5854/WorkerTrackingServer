using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.GetWorkerById;
public sealed record GetWorkerByIdCommand(Guid Id) : IRequest<Result<AppUser>>;
