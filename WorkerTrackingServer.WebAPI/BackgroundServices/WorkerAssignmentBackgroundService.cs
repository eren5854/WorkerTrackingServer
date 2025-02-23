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
        List<WorkerAssignment> workerAssignments = await workerAssignmentRepository.GetAll().Where(w => w.IsActive).ToListAsync();
        foreach(var item in workerAssignments)
        {
            item.ActualQuantity = null;
            item.EndTime = null;
            workerAssignmentRepository.Update(item);
        }
        await unitOfWork.SaveChangesAsync();
    }
}
