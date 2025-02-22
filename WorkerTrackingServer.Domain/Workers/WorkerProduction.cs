using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstracts;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Domain.Workers;
public sealed class WorkerProduction : Production
{
    //public Guid WorkerId { get; set; } = default!;
    //public Worker Worker { get; set; } = default!;

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
}
