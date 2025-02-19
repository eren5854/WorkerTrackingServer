using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Workers;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerLoginRepository : Repository<WorkerLogin, ApplicationDbContext>, IWorkerLoginRepository
{
    public WorkerLoginRepository(ApplicationDbContext context) : base(context)
    {
    }
}
