using ED.GenericRepository;
using ED.Result;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Application.Services;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Infrastructure.Services;
internal sealed class ProductionCalculateService(
    IWorkerProductionRepository workerProductionRepository,
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IWorkerWeeklyProductionRepository workerWeeklyProductionRepository,
    IUnitOfWork unitOfWork) : IProductionCalculateService
{
    public async Task<Result<string>> WeeklyProductionCalculate(CancellationToken cancellationToken)
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
            .ToListAsync(cancellationToken);

        foreach (var workerProduction in workerProductions)
        {
            // İşçiye ait günlük üretim verilerini çek
            List<WorkerDailyProduction> workerDailyProductions = await workerDailyProductionRepository
                .GetAll()
                .Where(w => w.WorkerProductionId == workerProduction.Id
                    && !w.IsActive
                    && w.DateStart >= startOfWeek
                    && w.DateStart <= endOfWeek)
                .ToListAsync(cancellationToken);

            if (workerDailyProductions.Count == 0)
                continue; // Eğer bu hafta için veri yoksa geç

            // Mevcut haftalık kaydı kontrol et
            var existingWeeklyProduction = await workerWeeklyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfWeek
                                          && w.DateEnd == endOfWeek, cancellationToken);

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

        await unitOfWork.SaveChangesAsync(cancellationToken); // Verileri kaydet

        return Result<string>.Succeed("Haftalık üretim hesaplaması tamamlandı.");
    }

    public async Task<Result<string>> WeeklyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken)
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
            .Where(w => w.Id == Id && w.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var workerProduction in workerProductions)
        {
            // İşçiye ait günlük üretim verilerini çek
            List<WorkerDailyProduction> workerDailyProductions = await workerDailyProductionRepository
                .GetAll()
                .Where(w => w.WorkerProductionId == workerProduction.Id
                    && !w.IsActive
                    && w.DateStart >= startOfWeek
                    && w.DateStart <= endOfWeek)
                .ToListAsync(cancellationToken);

            if (workerDailyProductions.Count == 0)
                continue; // Eğer bu hafta için veri yoksa geç

            // Mevcut haftalık kaydı kontrol et
            var existingWeeklyProduction = await workerWeeklyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfWeek
                                          && w.DateEnd == endOfWeek, cancellationToken);

            if (existingWeeklyProduction != null)
            {
                existingWeeklyProduction.WeeklyActual = workerDailyProductions.Sum(w => w.DailyActual);
                existingWeeklyProduction.WeeklyTarget = workerProduction.WeeklyTarget;
                existingWeeklyProduction.WeeklyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100;
                existingWeeklyProduction.UpdatedBy = "System";
                existingWeeklyProduction.UpdatedDate = DateTime.Now;

                workerWeeklyProductionRepository.Update(existingWeeklyProduction);

                workerProduction.WeeklyActual = workerDailyProductions.Sum(w => w.DailyActual);
                workerProduction.WeeklyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100;

                workerProductionRepository.Update(workerProduction);
            }
            else
            {
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

                workerProduction.WeeklyActual = workerDailyProductions.Sum(w => w.DailyActual);
                workerProduction.WeeklyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100;

                workerProductionRepository.Update(workerProduction);

                await workerWeeklyProductionRepository.AddAsync(workerWeeklyProduction);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken); // Verileri kaydet

        return Result<string>.Succeed("Haftalık üretim hesaplaması tamamlandı.");
    }
}
