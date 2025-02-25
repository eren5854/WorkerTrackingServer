using ED.GenericRepository;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.WebAPI.BackgroundServices;

public class WorkerProductionBackgroundService(
    IWorkerProductionRepository workerProductionRepository,
    IWorkerWeeklyProductionRepository workerWeeklyProductionRepository,
    IUnitOfWork unitOfWork)
{
    public void WorkerProductionDaily()
    {

    }

    public async Task WorkerProductionWeekly()
    {
        DateTime now = DateTime.Now;
        if (now.DayOfWeek == DayOfWeek.Sunday)
        {
            List<WorkerProduction> workerProductions = await workerProductionRepository.GetAll().ToListAsync();

            foreach (var workerProduction in workerProductions)
            {
                WorkerWeeklyProduction workerWeeklyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    WeeklyActual = workerProduction.WeeklyActual,
                    WeeklyTarget = workerProduction.WeeklyTarget,
                    WeeklyYield = workerProduction.WeeklyYield,
                    DateStart = DateTime.Now.Date.AddDays(-7), // Hatalı kod düzeltildi
                    DateEnd = DateTime.Now.Date,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };
                await workerWeeklyProductionRepository.AddAsync(workerWeeklyProduction);
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}
