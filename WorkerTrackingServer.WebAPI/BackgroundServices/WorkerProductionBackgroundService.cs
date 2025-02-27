using ED.GenericRepository;
using ED.Result;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Infrastructure.Repositories;

namespace WorkerTrackingServer.WebAPI.BackgroundServices;

public class WorkerProductionBackgroundService(
    IWorkerProductionRepository workerProductionRepository,
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IWorkerWeeklyProductionRepository workerWeeklyProductionRepository,
    IUnitOfWork unitOfWork)
{
    public void WorkerProductionDaily()
    {

    }

    public async Task WorkerProductionWeekly()
    {

        // Bugün hangi gün? (Pazartesi başlangıç, Pazar bitiş)
        DateTime today = DateTime.Now.Date;
        int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysSinceMonday < 0) daysSinceMonday += 7; // Eğer pazar günü çalıştırıldıysa
        DateTime startOfWeek = today.AddDays(-daysSinceMonday);  // Pazartesi
        DateTime endOfWeek = startOfWeek.AddDays(6);  // Pazar

        // Pasif olan işçi üretimlerini getir
        List<WorkerProduction> workerProductions = await workerProductionRepository
            .GetAll()
            .Where(w => w.IsActive)
            .ToListAsync();

        foreach (var workerProduction in workerProductions)
        {
            // İşçiye ait günlük üretim verilerini çek
            List<WorkerDailyProduction> workerDailyProductions = await workerDailyProductionRepository
                .GetAll()
                .Where(w => w.WorkerProductionId == workerProduction.Id
                    && !w.IsActive
                && w.DateStart >= startOfWeek
                    && w.DateStart <= endOfWeek)
                .ToListAsync();

            if (workerDailyProductions.Count == 0)
                continue; // Eğer bu hafta için veri yoksa geç

            // Mevcut haftalık kaydı kontrol et
            var existingWeeklyProduction = await workerWeeklyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfWeek
                                          && w.DateEnd == endOfWeek);

            if (existingWeeklyProduction != null)
            {
                // Güncellenmiş değerleri mevcut kayda ata
                existingWeeklyProduction.WeeklyActual = workerDailyProductions.Sum(w => w.DailyActual);
                existingWeeklyProduction.WeeklyTarget = workerProduction.WeeklyTarget;
                existingWeeklyProduction.WeeklyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100;
                existingWeeklyProduction.UpdatedBy = "System";
                existingWeeklyProduction.UpdatedDate = DateTime.Now;

                workerWeeklyProductionRepository.Update(existingWeeklyProduction);
            }
            else
            {
                // Yeni kayıt oluştur
                WorkerWeeklyProduction workerWeeklyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    WeeklyActual = workerDailyProductions.Sum(w => w.DailyActual),
                    WeeklyTarget = workerProduction.WeeklyTarget,
                    WeeklyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100,
                    DateStart = startOfWeek,
                    DateEnd = endOfWeek,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                await workerWeeklyProductionRepository.AddAsync(workerWeeklyProduction);
            }
        }

        unitOfWork.SaveChanges(); // Verileri kaydet

        //DateTime now = DateTime.Now;
        //if (now.DayOfWeek == DayOfWeek.Sunday)
        //{
        //    List<WorkerProduction> workerProductions = await workerProductionRepository.GetAll().ToListAsync();

        //    foreach (var workerProduction in workerProductions)
        //    {
        //        WorkerWeeklyProduction workerWeeklyProduction = new()
        //        {
        //            WorkerProductionId = workerProduction.Id,
        //            WeeklyActual = workerProduction.WeeklyActual,
        //            WeeklyTarget = workerProduction.WeeklyTarget,
        //            WeeklyYield = workerProduction.WeeklyYield,
        //            DateStart = DateTime.Now.Date.AddDays(-7), // Hatalı kod düzeltildi
        //            DateEnd = DateTime.Now.Date,
        //            IsActive = false,
        //            CreatedBy = "System",
        //            CreatedDate = DateTime.Now,
        //        };
        //        await workerWeeklyProductionRepository.AddAsync(workerWeeklyProduction);
        //    }
        //    await unitOfWork.SaveChangesAsync();
        //}
    }
}
