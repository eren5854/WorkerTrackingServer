using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Domain.Products;
public sealed class Product : Entity
{
    public string ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; }
    public string? ProductDescription { get; set; }

    public List<WorkerProduction>? WorkerProductions { get; set; }
}
