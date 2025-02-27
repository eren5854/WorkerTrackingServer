using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetAllWorkerAssignmentByAppUserId;
internal sealed class GetAllWorkerAssignmentByAppUserIdCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository) : IRequestHandler<GetAllWorkerAssignmentByAppUserIdCommand, Result<List<WorkerAssignment>>>
{
    public async Task<Result<List<WorkerAssignment>>> Handle(GetAllWorkerAssignmentByAppUserIdCommand request, CancellationToken cancellationToken)
    {
        List<WorkerAssignment> workerAssignments = await workerAssignmentRepository
            .GetAll()
            .Where(w => w.AppUserId == request.AppUserId)
            .Include(i => i.AppUser)
            .Include(i => i.Machine)
            .Include(i => i.WorkerProduction)
                .ThenInclude(t => t.AppUser)
            .Include(i => i.WorkerProduction)
                .ThenInclude(t => t.Product)
            .OrderByDescending(o => o.CreatedDate).ToListAsync(cancellationToken);

        return Result<List<WorkerAssignment>>.Succeed(workerAssignments);
    }
}
