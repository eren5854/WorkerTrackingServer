using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.DTOs;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetAllWorkerProduction;
internal sealed class GetAllWorkerProductionQueryHandler(
    IWorkerProductionRepository workerProductionRepository) : IRequestHandler<GetAllWorkerProductionQuery, Result<List<GetAllWorkerProductionDto>>>
{
    public async Task<Result<List<GetAllWorkerProductionDto>>> Handle(GetAllWorkerProductionQuery request, CancellationToken cancellationToken)
    {
        List<WorkerProduction> workerProductions = await workerProductionRepository
            .GetAll()
            .Include(i => i.AppUser)
            .Include(i => i.Product)
            .Include(i => i.DailyProductions!.OrderByDescending(o => o.DailyActual))
            .Include(i => i.WeeklyProductions!.OrderByDescending(o => o.WeeklyActual))
            .Include(i => i.MonthlyProductions!.OrderByDescending(o => o.MonthlyActual))
            .Include(i => i.YearlyProductions!.OrderByDescending(o => o.YearlyActual))
            .ToListAsync(cancellationToken);

        //var yearlyTotals = workerProductions
        //   .GroupBy(wp => wp.AppUserId) // appUserId'ye göre grupla
        //   .Select(group => new
        //   {
        //       AppUserId = group.Key,
        //       TotalYearlyActual = group.Sum(wp => wp.YearlyActual ?? 0) // null değerleri 0 olarak al
        //   })
        //   .ToList();

        var totals = workerProductions
            .GroupBy(wp => new { wp.AppUserId, wp.AppUser.FullName }) // Kullanıcıya göre grupla
            .Select(group => new GetAllWorkerProductionDto(
                AppUserId: group.Key.AppUserId, // Kullanıcının ID'si
                FullName: group.Key.FullName, // Kullanıcının adı
                Total: group.Sum(wp => wp.YearlyProductions?.Sum(yp => yp.YearlyActual) ?? 0) // YearlyActual toplamı
            ))
            .OrderByDescending(o => o.Total) // Büyükten küçüğe sıralama
            .ToList();

        return Result<List<GetAllWorkerProductionDto>>.Succeed(totals);
    }
}
