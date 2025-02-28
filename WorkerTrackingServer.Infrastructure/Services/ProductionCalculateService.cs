using ED.GenericRepository;
using ED.Result;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Application.Services;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Infrastructure.Repositories;

namespace WorkerTrackingServer.Infrastructure.Services;
internal sealed class ProductionCalculateService(
    IWorkerProductionRepository workerProductionRepository,
    IWorkerDailyProductionRepository workerDailyProductionRepository,
    IWorkerWeeklyProductionRepository workerWeeklyProductionRepository,
    IWorkerMonthlyProductionRepository workerMonthlyProductionRepository,
    IWorkerYearlyProductionRepository workerYearlyProductionRepository,
    IUnitOfWork unitOfWork) : IProductionCalculateService
{
    public async Task<Result<string>> MonthlyProductionCalculate(CancellationToken cancellationToken)
    {
        // Şu anki tarih
        DateTime today = DateTime.Now.Date;

        // Ayın ilk ve son gününü belirleme
        DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
        DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Gelecek ayın 1'inden 1 gün çıkar

        // Aktif işçi üretimlerini getir
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
                    && w.DateStart >= startOfMonth
                    && w.DateStart <= endOfMonth)
                .ToListAsync(cancellationToken);

            if (workerDailyProductions.Count == 0)
                continue; // Eğer bu ay için veri yoksa geç

            // Mevcut aylık kaydı kontrol et
            var existingMonthlyProduction = await workerMonthlyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfMonth
                                          && w.DateEnd == endOfMonth, cancellationToken);

            if (existingMonthlyProduction != null)
            {
                // Güncellenmiş değerleri mevcut kayda ata
                existingMonthlyProduction.MonthlyActual = workerDailyProductions.Sum(w => w.DailyActual);
                existingMonthlyProduction.MonthlyTarget = workerProduction.MonthlyTarget;
                existingMonthlyProduction.MonthlyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100;
                existingMonthlyProduction.UpdatedBy = "System";
                existingMonthlyProduction.UpdatedDate = DateTime.Now;

                workerMonthlyProductionRepository.Update(existingMonthlyProduction);
            }
            else
            {
                // Yeni kayıt oluştur
                WorkerMonthlyProduction workerMonthlyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    MonthlyActual = workerDailyProductions.Sum(w => w.DailyActual),
                    MonthlyTarget = workerProduction.MonthlyTarget,
                    MonthlyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100,
                    DateStart = startOfMonth,
                    DateEnd = endOfMonth,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                await workerMonthlyProductionRepository.AddAsync(workerMonthlyProduction);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken); // Verileri kaydet

        return Result<string>.Succeed("Aylık üretim hesaplaması tamamlandı.");
    }

    public async Task<Result<string>> MonthlyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken)
    {
        // Şu anki tarih
        DateTime today = DateTime.Now.Date;

        // Ayın ilk ve son gününü belirleme
        DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
        DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Gelecek ayın 1'inden 1 gün çıkar

        // Aktif işçi üretimlerini getir
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
                    && w.DateStart >= startOfMonth
                    && w.DateStart <= endOfMonth)
                .ToListAsync(cancellationToken);

            if (workerDailyProductions.Count == 0)
                continue; // Eğer bu ay için veri yoksa geç

            // Mevcut aylık kaydı kontrol et
            var existingMonthlyProduction = await workerMonthlyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfMonth
                                          && w.DateEnd == endOfMonth, cancellationToken);

            if (existingMonthlyProduction != null)
            {
                // Güncellenmiş değerleri mevcut kayda ata
                existingMonthlyProduction.MonthlyActual = workerDailyProductions.Sum(w => w.DailyActual);
                existingMonthlyProduction.MonthlyTarget = workerProduction.MonthlyTarget;
                existingMonthlyProduction.MonthlyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100;
                existingMonthlyProduction.UpdatedBy = "System";
                existingMonthlyProduction.UpdatedDate = DateTime.Now;

                workerMonthlyProductionRepository.Update(existingMonthlyProduction);

                workerProduction.MonthlyActual = workerDailyProductions.Sum(w => w.DailyActual);
                workerProduction.MonthlyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100;
                workerProductionRepository.Update(workerProduction);
            }
            else
            {
                // Yeni kayıt oluştur
                WorkerMonthlyProduction workerMonthlyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    MonthlyActual = workerDailyProductions.Sum(w => w.DailyActual),
                    MonthlyTarget = workerProduction.MonthlyTarget,
                    MonthlyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100,
                    DateStart = startOfMonth,
                    DateEnd = endOfMonth,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                workerProduction.MonthlyActual = workerDailyProductions.Sum(w => w.DailyActual);
                workerProduction.MonthlyYield = ((double)workerDailyProductions.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100;
                workerProductionRepository.Update(workerProduction);

                await workerMonthlyProductionRepository.AddAsync(workerMonthlyProduction);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken); // Verileri kaydet

        return Result<string>.Succeed("İşçinin aylık üretim hesaplaması tamamlandı.");
    }

    public async Task<Result<string>> ProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken)
    {
        DateTime today = DateTime.Now.Date;

        int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysSinceMonday < 0) daysSinceMonday += 7;
        
        DateTime startOfWeek = today.AddDays(-daysSinceMonday);
        DateTime endOfWeek = startOfWeek.AddDays(6);

        DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
        DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        DateTime startOfYear = new DateTime(today.Year, 1, 1);
        DateTime endOfYear = startOfYear.AddYears(1).AddDays(-1);

        List<WorkerProduction> workerProductions = await workerProductionRepository
            .GetAll()
            .Where(w => w.Id == Id && w.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var workerProduction in workerProductions)
        {
            List<WorkerDailyProduction> workerDailyProductionsWeek = await workerDailyProductionRepository
                .GetAll()
                .Where(w => w.WorkerProductionId == workerProduction.Id
                    && !w.IsActive
                    && w.DateStart >= startOfWeek
                    && w.DateStart <= endOfWeek)
                .ToListAsync(cancellationToken);

            List<WorkerDailyProduction> workerDailyProductionsMonth = await workerDailyProductionRepository
               .GetAll()
               .Where(w => w.WorkerProductionId == workerProduction.Id
                   && !w.IsActive
                   && w.DateStart >= startOfMonth
                   && w.DateStart <= endOfMonth)
               .ToListAsync(cancellationToken);

            List<WorkerMonthlyProduction> workerMonthlyProductions = await workerMonthlyProductionRepository
               .GetAll()
               .Where(w => w.WorkerProductionId == workerProduction.Id
                   && !w.IsActive
                   && w.DateStart >= startOfYear
                   && w.DateStart <= endOfYear)
               .ToListAsync(cancellationToken);

            //if (workerDailyProductionsWeek.Count == 0 || workerDailyProductionsMonth.Count == 0)
            //    continue;

            var existingWeeklyProduction = await workerWeeklyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfWeek
                                          && w.DateEnd == endOfWeek, cancellationToken);

            if (existingWeeklyProduction != null)
            {
                existingWeeklyProduction.WeeklyActual = workerDailyProductionsWeek.Sum(w => w.DailyActual);
                existingWeeklyProduction.WeeklyTarget = workerProduction.WeeklyTarget;
                existingWeeklyProduction.WeeklyYield = ((double)workerDailyProductionsWeek.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100;
                existingWeeklyProduction.UpdatedBy = "System";
                existingWeeklyProduction.UpdatedDate = DateTime.Now;

                workerWeeklyProductionRepository.Update(existingWeeklyProduction);
            }
            if(existingWeeklyProduction == null)
            {
                WorkerWeeklyProduction workerWeeklyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    WeeklyActual = workerDailyProductionsWeek.Sum(w => w.DailyActual),
                    WeeklyTarget = workerProduction.WeeklyTarget,
                    WeeklyYield = ((double)workerDailyProductionsWeek.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100,
                    DateStart = startOfWeek,
                    DateEnd = endOfWeek,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                await workerWeeklyProductionRepository.AddAsync(workerWeeklyProduction);
            }

            var existingMonthlyProduction = await workerMonthlyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfMonth
                                          && w.DateEnd == endOfMonth, cancellationToken);

            if (existingMonthlyProduction != null)
            {
                existingMonthlyProduction.MonthlyActual = workerDailyProductionsMonth.Sum(w => w.DailyActual);
                existingMonthlyProduction.MonthlyTarget = workerProduction.MonthlyTarget;
                existingMonthlyProduction.MonthlyYield = ((double)workerDailyProductionsMonth.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100;
                existingMonthlyProduction.UpdatedBy = "System";
                existingMonthlyProduction.UpdatedDate = DateTime.Now;

                workerMonthlyProductionRepository.Update(existingMonthlyProduction);
            }
            if(existingMonthlyProduction == null)
            {
                WorkerMonthlyProduction workerMonthlyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    MonthlyActual = workerDailyProductionsMonth.Sum(w => w.DailyActual),
                    MonthlyTarget = workerProduction.MonthlyTarget,
                    MonthlyYield = ((double)workerDailyProductionsMonth.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100,
                    DateStart = startOfMonth,
                    DateEnd = endOfMonth,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                await workerMonthlyProductionRepository.AddAsync(workerMonthlyProduction);
            }

            var existingYearlyProduction = await workerYearlyProductionRepository
               .GetAll()
               .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                         && w.DateStart == startOfYear
                                         && w.DateEnd == endOfYear, cancellationToken);

            if (existingYearlyProduction != null)
            {
                existingYearlyProduction.YearlyActual = workerMonthlyProductions.Sum(w => w.MonthlyActual);
                existingYearlyProduction.YearlyTarget = workerProduction.YearlyTarget;
                existingYearlyProduction.YearlyYield = ((double)workerMonthlyProductions.Sum(w => w.MonthlyActual)! / workerProduction.YearlyTarget) * 100;
                existingYearlyProduction.UpdatedBy = "System";
                existingYearlyProduction.UpdatedDate = DateTime.Now;

                workerYearlyProductionRepository.Update(existingYearlyProduction);
            }
            if(existingYearlyProduction == null)
            {
                WorkerYearlyProduction workerYearlyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    YearlyActual = workerMonthlyProductions.Sum(w => w.MonthlyActual),
                    YearlyTarget = workerProduction.YearlyTarget,
                    YearlyYield = ((double)workerMonthlyProductions.Sum(w => w.MonthlyActual)! / workerProduction.YearlyTarget) * 100,
                    DateStart = startOfYear,
                    DateEnd = endOfYear,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                await workerYearlyProductionRepository.AddAsync(workerYearlyProduction);
            }

            workerProduction.WeeklyActual = workerDailyProductionsWeek.Sum(w => w.DailyActual);
            workerProduction.WeeklyYield = ((double)workerDailyProductionsWeek.Sum(w => w.DailyActual)! / workerProduction.WeeklyTarget) * 100;
            workerProduction.MonthlyActual = workerDailyProductionsMonth.Sum(w => w.DailyActual);
            workerProduction.MonthlyYield = ((double)workerDailyProductionsMonth.Sum(w => w.DailyActual)! / workerProduction.MonthlyTarget) * 100;
            workerProduction.YearlyActual = workerMonthlyProductions.Sum(w => w.MonthlyActual);
            workerProduction.YearlyYield = ((double)workerMonthlyProductions.Sum(w => w.MonthlyActual)! / workerProduction.YearlyTarget) * 100;

            workerProductionRepository.Update(workerProduction);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Haftalık ve aylık üretim hesaplaması tamamlandı.");
    }

    public async Task<Result<string>> WeeklyProductionCalculate(CancellationToken cancellationToken)
    {
        // Bugün hangi gün? (Pazartesi başlangıç, Pazar bitiş)
        DateTime today = DateTime.Now.Date;
        int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysSinceMonday < 0) daysSinceMonday += 7; // Eğer pazar günü çalıştırıldıysa
        DateTime startOfWeek = today.AddDays(-daysSinceMonday);  // Pazartesi
        DateTime endOfWeek = startOfWeek.AddDays(6);  // Pazar

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

    public async Task<Result<string>> YearlyProductionCalculate(CancellationToken cancellationToken)
    {
        DateTime today = DateTime.Now.Date;

        DateTime startOfYear = new DateTime(today.Year, 1, 1);
        DateTime endOfYear = startOfYear.AddYears(1).AddDays(-1);

        List<WorkerProduction> workerProductions = await workerProductionRepository
            .GetAll()
            .Where(w => w.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var workerProduction in workerProductions)
        {
            List<WorkerMonthlyProduction> workerMonthlyProductions = await workerMonthlyProductionRepository
                .GetAll()
                .Where(w => w.WorkerProductionId == workerProduction.Id
                    && !w.IsActive
                    && w.DateStart >= startOfYear
                    && w.DateStart <= endOfYear)
                .ToListAsync(cancellationToken);

            if (workerMonthlyProductions.Count == 0)
                continue;

            var existingYearlyProduction = await workerYearlyProductionRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.WorkerProductionId == workerProduction.Id
                                          && w.DateStart == startOfYear
                                          && w.DateEnd == endOfYear, cancellationToken);

            if (existingYearlyProduction != null)
            {
                existingYearlyProduction.YearlyActual = workerMonthlyProductions.Sum(w => w.MonthlyActual);
                existingYearlyProduction.YearlyTarget = workerProduction.MonthlyTarget;
                existingYearlyProduction.YearlyYield = ((double)workerMonthlyProductions.Sum(w => w.MonthlyActual)! / workerProduction.YearlyTarget) * 100;
                existingYearlyProduction.UpdatedBy = "System";
                existingYearlyProduction.UpdatedDate = DateTime.Now;

                workerYearlyProductionRepository.Update(existingYearlyProduction);

                workerProduction.YearlyActual = workerMonthlyProductions.Sum(w => w.MonthlyActual);
                workerProduction.YearlyYield = ((double)workerMonthlyProductions.Sum(w => w.MonthlyActual)! / workerProduction.YearlyTarget) * 100;
                workerProductionRepository.Update(workerProduction);
            }
            else
            {
                WorkerYearlyProduction workerYearlyProduction = new()
                {
                    WorkerProductionId = workerProduction.Id,
                    YearlyActual = workerMonthlyProductions.Sum(w => w.MonthlyActual),
                    YearlyTarget = workerProduction.MonthlyTarget,
                    YearlyYield = ((double)workerMonthlyProductions.Sum(w => w.MonthlyActual)! / workerProduction.YearlyTarget) * 100,
                    DateStart = startOfYear,
                    DateEnd = endOfYear,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                await workerYearlyProductionRepository.AddAsync(workerYearlyProduction);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Yıllık üretim hesaplaması tamamlandı.");
    }

    public async Task<Result<string>> YearlyProductionCalculateByWorkerProductionId(Guid Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
