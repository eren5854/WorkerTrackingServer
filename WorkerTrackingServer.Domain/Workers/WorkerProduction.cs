using WorkerTrackingServer.Domain.Abstracts;
using WorkerTrackingServer.Domain.Products;

namespace WorkerTrackingServer.Domain.Workers;
public sealed class WorkerProduction : Production
{
    public Guid WorkerId { get; set; } = default!;
    public Worker Worker { get; set; } = default!;

    public Guid ProductId { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
