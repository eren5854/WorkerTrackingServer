using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerActualQuantity;
internal sealed class WorkerActualQuantityCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IWorkerProductionRepository workerProductionRepository,
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
            return Result<string>.Failure("Mesai bitiş saati girildiği için değişiklik yapamazsınız. Lütfen yöneticinize başvurun");
        }

        

        workerAssignment.ActualQuantity = request.ActualQuantity;
        workerAssignment.EndTime = DateTime.Now;
        workerAssignment.UpdatedBy = workerAssignment.AppUser.FullName;
        workerAssignment.UpdatedDate = DateTime.Now;
        workerAssignment.IsActive = false;

        WorkerProduction workerProduction = await workerProductionRepository.GetByExpressionAsync(g => g.AppUserId == request.AppUserId && g.ProductId == workerAssignment.ProductId && g.IsActive, cancellationToken);
        if (workerProduction is null)
        {
            return Result<string>.Failure("Worker production not found");
        }
        workerProduction.DailyActual = request.ActualQuantity;
        workerProduction.WeeklyActual += request.ActualQuantity;
        workerProduction.MonthlyActual += request.ActualQuantity;
        workerProduction.YearlyActual += request.ActualQuantity;
        workerProductionRepository.Update(workerProduction);

        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Actual Quantity Saved");
    }
}
