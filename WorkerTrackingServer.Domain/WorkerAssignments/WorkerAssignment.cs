using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Domain.WorkerAssignments;
public sealed class WorkerAssignment : Entity
{
    //public Guid? WorkerId { get; set; }
    //public Worker? Worker { get; set; }

    public object? UserInfo => new
    {
        AppUserId = AppUserId,
        FullName = AppUser.FullName,
    };

    [JsonIgnore]
    public Guid AppUserId { get; set; } = default!;
    [JsonIgnore]
    public AppUser AppUser { get; set; } = default!;

    public object? MachineInfo => new
    {
        MachineId = MachineId,
        MachineName = Machine?.MachineName,
        MachineNumber = Machine?.MachineNumber,
    };

    [JsonIgnore]
    public Guid? MachineId { get; set; }
    [JsonIgnore]
    public Machine? Machine { get; set; }

    public object? ProductInfo => new
    {
        ProductId = ProductId,
        ProductName = Product?.ProductName,
        ProductCode = Product?.ProductCode,
    };

    [JsonIgnore]
    public Guid? ProductId { get; set; }
    [JsonIgnore]
    public Product? Product { get; set; }

    public int TargetQuantity { get; set; }
    public int? ActualQuantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
