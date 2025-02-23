using ED.GenericRepository;
using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;
using WorkerTrackingServer.Domain.WorkerAssignments;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerLoginWithWorkerCode;
internal sealed class WorkerLoginWithWorkerCodeCommandHandler(
    IAppUserRepository appUserRepository,
    IWorkerAssignmentRepository workerAssignmentRepository,
    IWorkerProductionRepository workerProductionRepository,
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<WorkerLoginWithWorkerCodeCommand, Result<WorkerAssignment>>
{
    public async Task<Result<WorkerAssignment>> Handle(WorkerLoginWithWorkerCodeCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = await appUserRepository.GetByExpressionAsync(g => g.WorkerCode == request.WorkerCode, cancellationToken);
        if (appUser is null)
        {
            return Result<WorkerAssignment>.Failure("Worker not found");
        }

        WorkerAssignment? workerAssignment = await workerAssignmentRepository.Where(w => w.AppUserId == appUser.Id && w.IsActive).Include(i => i.AppUser).Include(i => i.Machine).Include(i => i.Product).FirstOrDefaultAsync(cancellationToken);
        if (workerAssignment is null)
        {
            return Result<WorkerAssignment>.Failure("Worker Assignment not found");
        }

        if(workerAssignment.StartTime.Date != DateTime.Now.Date)
        {
            return Result<WorkerAssignment>.Failure("Bu gün için görev tanımlanmamış");
        }

        WorkerProduction workerProduction = await workerProductionRepository.GetByExpressionAsync(g => g.AppUserId == appUser.Id && g.ProductId == workerAssignment.ProductId && g.IsActive, cancellationToken);
        if (workerProduction is null)
        {
            return Result<WorkerAssignment>.Failure("Worker Production bulunamadı. lütfen yöneticinize başvurun");
        }

        bool isWorkerDailyProductionExists = await workerDailyProductionRepository.AnyAsync(a => a.DateStart.Date == DateTime.Now.Date, cancellationToken);
        if (!isWorkerDailyProductionExists)
        {
            WorkerDailyProduction workerDailyProduction = new()
            {
                WorkerProductionId = workerProduction.Id,
                DailyTarget = workerAssignment.TargetQuantity,
                DateStart = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedBy = appUser.FullName,
                IsActive = true
            };
            await workerDailyProductionRepository.AddAsync(workerDailyProduction, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return Result<WorkerAssignment>.Succeed(workerAssignment);
    }
}
