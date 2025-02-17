using WorkerTrackingServer.Domain.Abstracts;
using WorkerTrackingServer.Domain.Products;

namespace WorkerTrackingServer.Domain.Departments;
public sealed class DepartmentProduction : Production
{
    public Guid DepartmentId { get; set; } = default!;
    public Department Department { get; set; } = default!;

    public Guid ProductId { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
