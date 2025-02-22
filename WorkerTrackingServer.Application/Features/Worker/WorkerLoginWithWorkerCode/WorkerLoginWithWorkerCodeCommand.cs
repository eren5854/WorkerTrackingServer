using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerLoginWithWorkerCode;
public sealed record WorkerLoginWithWorkerCodeCommand(
    string WorkerCode) : IRequest<Result<WorkerAssignment>>;
