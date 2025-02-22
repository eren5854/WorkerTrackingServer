using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.DeleteWorkerAssignmentById;
public sealed record DeleteWorkerAssignmentByIdCommand(Guid Id) : IRequest<Result<string>>;