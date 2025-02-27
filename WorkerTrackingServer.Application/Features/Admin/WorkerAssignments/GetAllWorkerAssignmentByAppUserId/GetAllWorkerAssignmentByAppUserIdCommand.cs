using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetAllWorkerAssignmentByAppUserId;
public sealed record GetAllWorkerAssignmentByAppUserIdCommand(Guid AppUserId) : IRequest<Result<List<WorkerAssignment>>>;
