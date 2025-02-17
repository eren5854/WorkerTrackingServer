using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.Workers;
public sealed class WorkerLogin : Entity
{
    public object WorkerInfo => new
    {
        WorkerId,
        WorkerFullName = Worker.FullName,
        WorkerDepartment = Worker.DepartmentInfo
    };

    [JsonIgnore]
    public Guid WorkerId { get; set; }
    [JsonIgnore]
    public Worker Worker { get; set; } = default!;

    public DateTime LoginTime { get; set; }
    public DateTime? LogoutTime { get; set; }
}
