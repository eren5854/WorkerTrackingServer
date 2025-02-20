using ED.GenericRepository;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class DepartmentRepository : Repository<Department, ApplicationDbContext>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
