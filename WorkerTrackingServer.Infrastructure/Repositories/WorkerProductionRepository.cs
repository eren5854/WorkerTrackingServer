using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Workers;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerProductionRepository : Repository<WorkerProduction, ApplicationDbContext>, IWorkerProductionRepository
{
    public WorkerProductionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
