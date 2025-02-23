using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetAllWorkerAssignment;
internal sealed class GetAllWorkerAssignmentQueryHandler(
    IWorkerAssignmentRepository workerAssignmentRepository) : IRequestHandler<GetAllWorkerAssignmentQuery, Result<List<WorkerAssignment>>>
{
    public async Task<Result<List<WorkerAssignment>>> Handle(GetAllWorkerAssignmentQuery request, CancellationToken cancellationToken)
    {
        List<WorkerAssignment> workerAssignments = await workerAssignmentRepository.GetAll().Include(i => i.AppUser).Include(i => i.Machine).Include(i => i.Product).OrderByDescending(o => o.CreatedDate).ToListAsync(cancellationToken);
        return Result<List<WorkerAssignment>>.Succeed(workerAssignments);
    }
}
