using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerAssignmentRepository : Repository<WorkerAssignment, ApplicationDbContext>, IWorkerAssignmentRepository
{
    public WorkerAssignmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
