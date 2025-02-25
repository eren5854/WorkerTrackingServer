using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Domain.Machines;
public sealed class Machine : Entity
{
    public string? MachineName { get; set; }
    public string? MachineBrand { get; set; }
    public string? MachineModel { get; set; }
    public string? MachineSerialNumber { get; set; }
    public string? MachineType { get; set; }
    public string? MachineLocation { get; set; }
    public bool? MachineStatus { get; set; }
    public int MachineNumber { get; set; }
    public string? MachineDescription { get; set; }

    public List<WorkerAssignment>? WorkerAssignments { get; set; }
}
