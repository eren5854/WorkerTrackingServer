using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Domain.Workers;
public sealed class WorkerLogin : Entity
{
    public object WorkerInfo => new
    {
        AppUserId,
        WorkerFullName = AppUser.FullName,
        WorkerDepartment = AppUser.DepartmentInfo
    };

    //[JsonIgnore]
    //public Guid WorkerId { get; set; }
    //[JsonIgnore]
    //public Worker Worker { get; set; } = default!;

    [JsonIgnore]
    public Guid AppUserId { get; set; }
    [JsonIgnore]
    public AppUser AppUser { get; set; } = default!;

    public DateTime LoginTime { get; set; }
    public DateTime? LogoutTime { get; set; }
}
