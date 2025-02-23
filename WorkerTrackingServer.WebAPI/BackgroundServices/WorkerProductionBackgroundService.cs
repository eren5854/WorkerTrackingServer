using ED.GenericRepository;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.WebAPI.BackgroundServices;

public class WorkerProductionBackgroundService(
    IWorkerProductionRepository workerProductionRepository,
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IUnitOfWork unitOfWork)
{
    public void WorkerProductionDaily()
    {
        
    }

    public async void WorkerProductionWeekly()
    {
        DateTime now = DateTime.Now;
        if(now.DayOfWeek == DayOfWeek.Sunday)
        {
            
        }
    }
}
