using ED.GenericRepository;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.WebAPI.BackgroundServices;

public class WorkerAssignmentBackgroundService(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IUnitOfWork unitOfWork)
{
    public async Task WorkerAssignmentReset()
    {
        List<WorkerAssignment> workerAssignments = await workerAssignmentRepository.GetAll().Where(w => w.IsActive).Include(i => i.WorkerProduction).ToListAsync();
        foreach(var item in workerAssignments)
        {
            item.WorkerProduction.DailyActual = 0;
            item.EndTime = null;
            workerAssignmentRepository.Update(item);
        }
        await unitOfWork.SaveChangesAsync();
    }
}
