using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Domain.WorkerAssignments;
public sealed class WorkerAssignment : Entity
{
    //public Guid? WorkerId { get; set; }
    //public Worker? Worker { get; set; }

    public Guid AppUserId { get; set; } = default!;
    public AppUser AppUser { get; set; } = default!;

    public Guid? MachineId { get; set; }
    public Machine? Machine { get; set; }

    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }

    public int TargetQuantity { get; set; }
    public int? ActualQuantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
