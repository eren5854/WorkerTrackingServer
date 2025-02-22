using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetWorkerAssignmentById;
internal sealed class GetWorkerAssignmentByIdCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository) : IRequestHandler<GetWorkerAssignmentByIdCommand, Result<WorkerAssignment>>
{
    public async Task<Result<WorkerAssignment>> Handle(GetWorkerAssignmentByIdCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment workerAssignment = await workerAssignmentRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (workerAssignment is null)
        {
            return Result<WorkerAssignment>.Failure("Worker assignment not found");
        }
        return Result<WorkerAssignment>.Succeed(workerAssignment);
    }
}
