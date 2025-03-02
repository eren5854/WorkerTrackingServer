using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.DTOs;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.Products.GetAllProductCount;
internal sealed class GetAllProductCountQueryHandler(
    IWorkerProductionRepository workerProductionRepository) : IRequestHandler<GetAllProductCountQuery, Result<List<GetAllProductCountDto>>>
{
    public async Task<Result<List<GetAllProductCountDto>>> Handle(GetAllProductCountQuery request, CancellationToken cancellationToken)
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

        var totals = workerProductions
            .GroupBy(wp => new { wp.ProductId, wp.Product.ProductName }) // Kullanıcıya göre grupla
            .Select(group => new GetAllProductCountDto(
                ProductId: group.Key.ProductId, // Kullanıcının ID'si
                Name: group.Key.ProductName, // Kullanıcının adı
                Count: group.Sum(wp => wp.YearlyProductions?.Sum(yp => yp.YearlyActual) ?? 0) // YearlyActual toplamı
            ))
            .OrderByDescending(o => o.Count) // Büyükten küçüğe sıralama
            .ToList();

        return Result<List<GetAllProductCountDto>>.Succeed(totals);
    }
}
