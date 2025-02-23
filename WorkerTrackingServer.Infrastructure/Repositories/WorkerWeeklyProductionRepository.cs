using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerWeeklyProductionRepository : Repository<WorkerWeeklyProduction, ApplicationDbContext>, IWorkerWeeklyProductionRepository
{
    public WorkerWeeklyProductionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
