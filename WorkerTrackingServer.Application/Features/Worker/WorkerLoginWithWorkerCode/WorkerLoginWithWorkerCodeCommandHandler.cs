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

        WorkerAssignment? workerAssignment = await workerAssignmentRepository
            .Where(w => w.AppUserId == appUser.Id && w.IsActive)
            .Include(i => i.AppUser)
            .Include(i => i.Machine)
            .Include(i => i.WorkerProduction)
                .ThenInclude(t => t.AppUser)
            .Include(i => i.WorkerProduction)
                .ThenInclude(t => t.Product)
            .FirstOrDefaultAsync(cancellationToken);
        if (workerAssignment is null)
        {
            return Result<WorkerAssignment>.Failure("Worker Assignment not found");
        }

        if(workerAssignment.StartTime.Date != DateTime.Now.Date)
        {
            return Result<WorkerAssignment>.Failure("Bu gün için görev tanımlanmamış");
        }

        if (workerAssignment.StartTime > DateTime.Now)
        {
            return Result<WorkerAssignment>.Failure("Tanımlanmış görev mesai başlangıcında gözükecektir");
        }

        bool isWorkerDailyProductionExists = await workerDailyProductionRepository.AnyAsync(a => a.WorkerProductionId == workerAssignment.WorkerProductionId && a.DateStart.Date == DateTime.Now.Date, cancellationToken);
        if (!isWorkerDailyProductionExists)
        {
            WorkerDailyProduction workerDailyProduction = new()
            {
                WorkerProductionId = workerAssignment.WorkerProductionId,
                DailyTarget = workerAssignment.WorkerProduction?.DailyTarget,
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
