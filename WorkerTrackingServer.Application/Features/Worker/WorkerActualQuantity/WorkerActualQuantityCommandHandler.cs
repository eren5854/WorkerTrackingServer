using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerActualQuantity;
internal sealed class WorkerActualQuantityCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<WorkerActualQuantityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(WorkerActualQuantityCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment workerAssignment = await workerAssignmentRepository.GetByExpressionAsync(g => g.AppUserId == request.AppUserId, cancellationToken);
        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found because worker not found");
        }

        if(workerAssignment.AppUser.IsActive is false)
        {
            return Result<string>.Failure("Worker is deactive");
        }

        if (workerAssignment.EndTime is not null)
        {
            return Result<string>.Failure("Mesai saati girildiği için değişiklik yapamazsınız. Lütfen yöneticinize başvurun");
        }

        workerAssignment.ActualQuantity = request.ActualQuantity;
        workerAssignment.EndTime = DateTime.Now;
        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Actual Quantity Saved");
    }
}
