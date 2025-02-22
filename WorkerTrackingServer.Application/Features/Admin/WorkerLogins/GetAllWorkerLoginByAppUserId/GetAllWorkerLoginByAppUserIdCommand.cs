using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerLogins.GetAllWorkerLoginByAppUserId;
public sealed record GetAllWorkerLoginByAppUserIdCommand(Guid AppUserId) : IRequest<Result<List<WorkerLogin>>>;
