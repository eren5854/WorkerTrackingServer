using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.DeleteWorkerAssignmentById;
internal sealed class DeleteWorkerAssignmentByIdCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteWorkerAssignmentByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteWorkerAssignmentByIdCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment workerAssignment = await workerAssignmentRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found");
        }

        workerAssignment.IsDeleted = true;

        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Worker assignment deleted successfully");
    }
}
