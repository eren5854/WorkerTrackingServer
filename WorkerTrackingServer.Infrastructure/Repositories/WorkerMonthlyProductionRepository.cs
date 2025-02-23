using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerMonthlyProductionRepository : Repository<WorkerMonthlyProduction, ApplicationDbContext>, IWorkerMonthlyProductionRepository
{
    public WorkerMonthlyProductionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
