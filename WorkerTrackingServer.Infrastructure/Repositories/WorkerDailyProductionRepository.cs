using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerDailyProductionRepository : Repository<WorkerDailyProduction, ApplicationDbContext>, IWorkerDailyProductionRepository
{
    public WorkerDailyProductionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
