using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateStatusWorkerAssignment;
internal sealed class UpdateStatusWorkerAssignmentCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateStatusWorkerAssignmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateStatusWorkerAssignmentCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment workerAssignment = await workerAssignmentRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found");
        }

        workerAssignment.IsActive = !workerAssignment.IsActive;
        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Worker assignment status update");
    }
}
