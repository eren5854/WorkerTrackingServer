using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerLoginWithWorkerCode;
internal sealed class WorkerLoginWithWorkerCodeCommandHandler(
    IAppUserRepository appUserRepository,
    IWorkerAssignmentRepository workerAssignmentRepository) : IRequestHandler<WorkerLoginWithWorkerCodeCommand, Result<WorkerAssignment>>
{
    public async Task<Result<WorkerAssignment>> Handle(WorkerLoginWithWorkerCodeCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = await appUserRepository.GetByExpressionAsync(g => g.WorkerCode == request.WorkerCode, cancellationToken);
        if (appUser is null)
        {
            return Result<WorkerAssignment>.Failure("Worker not found");
        }

        WorkerAssignment? workerAssignment = await workerAssignmentRepository.Where(w => w.AppUserId == appUser.Id).FirstOrDefaultAsync(cancellationToken);
        if (workerAssignment is null)
        {
            return Result<WorkerAssignment>.Failure("Worker Assignment not found");
        }

        if(workerAssignment.StartTime != DateTime.Now.Date)
        {
            return Result<WorkerAssignment>.Failure("Bu gün için görev tanımlanmamış");
        }

        return Result<WorkerAssignment>.Succeed(workerAssignment);
    }
}
