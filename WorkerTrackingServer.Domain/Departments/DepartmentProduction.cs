using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstracts;
using WorkerTrackingServer.Domain.Products;

namespace WorkerTrackingServer.Domain.Departments;
public sealed class DepartmentProduction : Production
{
    public object DepartmentInfo => new
    {
        DepartmentId = Department.Id,
        DepartmentName = Department.DepartmentName,
        DepartmentDescription = Department.DepartmentDescription,
    };

    [JsonIgnore]
    public Guid DepartmentId { get; set; } = default!;
    [JsonIgnore]
    public Department Department { get; set; } = default!;

    [JsonIgnore]
    public Guid ProductId { get; set; } = default!;
    [JsonIgnore]
    public Product Product { get; set; } = default!;

    public object? ProductInfo => new
    {
        ProductId = Product.Id,
        ProductName = Product.ProductName,
        ProductCode = Product.ProductCode,
        ProductDescription = Product.ProductDescription
    };
}
