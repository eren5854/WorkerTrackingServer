using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetAllWorkerAssignment;
public sealed record GetAllWorkerAssignmentQuery() : IRequest<Result<List<WorkerAssignment>>>;
