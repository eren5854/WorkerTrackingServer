using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.GetAllWorker;
public sealed record GetAllWorkerQuery() : IRequest<Result<List<AppUser>>>;
