using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class WorkerYearlyProductionRepository : Repository<WorkerYearlyProduction, ApplicationDbContext>, IWorkerYearlyProductionRepository
{
    public WorkerYearlyProductionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
