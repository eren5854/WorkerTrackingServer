using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstracts;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Users;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Domain.WorkerProductions;
public sealed class WorkerProduction : Production
{
    public object AppUserInfo => new
    {
        AppUserId = AppUserId,
        FullName = AppUser.FullName,
    };

    [JsonIgnore]
    public Guid AppUserId { get; set; } = default!;
    [JsonIgnore]
    public AppUser AppUser { get; set; } = default!;

    public object ProductInfo => new
    {
        ProductId = ProductId,
        ProductName = Product.ProductName,
        ProductCode = Product.ProductCode,
    };

    [JsonIgnore]
    public Guid ProductId { get; set; } = default!;
    [JsonIgnore]
    public Product Product { get; set; } = default!;

    public List<WorkerDailyProduction>? DailyProductions { get; set; }
    public List<WorkerWeeklyProduction>? WeeklyProductions { get; set; }
    public List<WorkerMonthlyProduction>? MonthlyProductions { get; set;}
    public List<WorkerYearlyProduction>? WorkerYearlyProductions { get; set; }

    public List<WorkerAssignment>? WorkerAssignments { get; set; }
}
