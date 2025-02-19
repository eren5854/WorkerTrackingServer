using ED.GenericRepository;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class DepartmentProductionRepository : Repository<DepartmentProduction, ApplicationDbContext>, IDepartmentProductionRepository
{
    public DepartmentProductionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
