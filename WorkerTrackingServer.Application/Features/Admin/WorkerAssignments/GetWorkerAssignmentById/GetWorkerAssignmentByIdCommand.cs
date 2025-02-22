using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetWorkerAssignmentById;
public sealed record GetWorkerAssignmentByIdCommand(Guid Id) : IRequest<Result<WorkerAssignment>>;
