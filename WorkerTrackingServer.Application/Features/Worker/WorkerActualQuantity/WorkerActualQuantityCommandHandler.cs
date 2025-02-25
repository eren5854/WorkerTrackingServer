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
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<WorkerActualQuantityCommand, Result<string>>
{
    public async Task<Result<string>> Handle(WorkerActualQuantityCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment? workerAssignment = await workerAssignmentRepository
            .Where(w => w.AppUserId == request.AppUserId)
            .Include(i => i.AppUser)
            .Include(i => i.Machine)
            .Include(i => i.WorkerProduction).FirstOrDefaultAsync(cancellationToken);
        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found because worker not found");
        }

        if(!workerAssignment.AppUser.IsActive)
        {
            return Result<string>.Failure("Worker is deactive");
        }

        if (workerAssignment.EndTime > DateTime.Now)
        {
            return Result<string>.Failure("Mesai bitiş saati girildiği için değişiklik yapamazsınız. Lütfen yöneticinize başvurun");
        }

        workerAssignment.WorkerProduction!.DailyActual = request.ActualQuantity;
        workerAssignment.EndTime = DateTime.Now;
        workerAssignment.UpdatedBy = workerAssignment.AppUser.FullName;
        workerAssignment.UpdatedDate = DateTime.Now;
        workerAssignment.WorkerProduction.WeeklyActual += request.ActualQuantity;
        workerAssignment.WorkerProduction.WeeklyYield = ((double)(workerAssignment.WorkerProduction.WeeklyActual!) / workerAssignment.WorkerProduction.WeeklyTarget) * 100;
        //workerAssignment.IsActive = false;

        WorkerDailyProduction workerDailyProduction = await workerDailyProductionRepository.GetByExpressionAsync(g => g.WorkerProductionId == workerAssignment.WorkerProductionId && g.IsActive, cancellationToken);
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
