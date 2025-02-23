using ED.GenericRepository;
using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerActualQuantity;
internal sealed class WorkerActualQuantityCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IWorkerProductionRepository workerProductionRepository,
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<WorkerActualQuantityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(WorkerActualQuantityCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment? workerAssignment = await workerAssignmentRepository.Where(w => w.AppUserId == request.AppUserId).Include(i => i.AppUser).Include(i => i.Machine).Include(i => i.Product).FirstOrDefaultAsync(cancellationToken);
        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found because worker not found");
        }

        if(!workerAssignment.AppUser.IsActive)
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
        //workerAssignment.IsActive = false;

        WorkerProduction workerProduction = await workerProductionRepository.GetByExpressionAsync(g => g.AppUserId == request.AppUserId && g.ProductId == workerAssignment.ProductId && g.IsActive, cancellationToken);
        if (workerProduction is null)
        {
            return Result<string>.Failure("Worker production not found");
        }

        WorkerDailyProduction workerDailyProduction = await workerDailyProductionRepository.GetByExpressionAsync(g => g.WorkerProductionId == workerProduction.Id && g.IsActive, cancellationToken);
        if (workerDailyProduction is null)
        {
            return Result<string>.Failure("Worker daily production not found");
        }

        workerDailyProduction.DailyActual = request.ActualQuantity;
        workerDailyProduction.DailyYield = ((double)request.ActualQuantity / workerDailyProduction.DailyTarget) * 100;
        workerDailyProduction.DateEnd = DateTime.Now;
        workerDailyProduction.IsActive = false;

        workerDailyProductionRepository.Update(workerDailyProduction);

        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Actual Quantity Saved");
    }
}
