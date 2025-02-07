using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.Machines;
public sealed class Machine : Entity
{
    public string? MachineName { get; set; }
    public string? MachineBrand { get; set; }
    public string? MachineModel { get; set; }
    public string? MachineSerialNumber { get; set; }
    public string? MachineType { get; set; }
    public string? MachineLocation { get; set; }
    public string? MachineStatus { get; set; }
    public int MachineNumber { get; set; }
    public string? MachineDescription { get; set; }
}
