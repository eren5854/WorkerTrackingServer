using ED.GenericRepository;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class MachineRepository : Repository<Machine, ApplicationDbContext>, IMachineRepository
{
    public MachineRepository(ApplicationDbContext context) : base(context)
    {
    }
}
