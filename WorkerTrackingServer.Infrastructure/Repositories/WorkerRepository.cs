using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Workers;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerRepository : Repository<Worker, ApplicationDbContext>, IWorkerRepository
{
    public WorkerRepository(ApplicationDbContext context) : base(context)
    {
    }
}
