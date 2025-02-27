using ED.GenericRepository;
using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateStatusWorkerAssignment;
internal sealed class UpdateStatusWorkerAssignmentCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateStatusWorkerAssignmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateStatusWorkerAssignmentCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment workerAssignment = await workerAssignmentRepository
            .GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);

        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found");
        }

        // Kullanıcının aktif başka bir ataması var mı?
        WorkerAssignment? activeWorkerAssignment = await workerAssignmentRepository
            .GetAll()
            .Where(a => a.Id != workerAssignment.Id && a.AppUserId == workerAssignment.AppUserId && a.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (activeWorkerAssignment is not null)
        {
            // Önce diğer aktif olanı pasif yap
            activeWorkerAssignment.IsActive = false;
            workerAssignmentRepository.Update(activeWorkerAssignment);

            // Sonra mevcut olanı aktif yap
            workerAssignment.IsActive = true;
        }
        else
        {
            // Eğer başka aktif kayıt yoksa mevcut olan pasif kalmalı
            workerAssignment.IsActive = !workerAssignment.IsActive;
        }

        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker assignment status updated successfully.");
    }
}
