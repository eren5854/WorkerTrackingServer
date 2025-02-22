using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateStatusWorkerAssignment;
public sealed record UpdateStatusWorkerAssignmentCommand(Guid Id) : IRequest<Result<string>>;
