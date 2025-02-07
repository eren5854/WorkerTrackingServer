using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Enums;

namespace WorkerTrackingServer.Domain.Workers;
public sealed class Worker : Entity
{
    public string WorkerCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string? ProfilePicture { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GenderSmartEnum Gender { get; set; } = GenderSmartEnum.Other;

    public object DepartmentInfo => new
    {
        DepartmentId = DepartmentId,
        DepartmentName = Department?.DepartmentName
    };

    [JsonIgnore]
    public Guid? DepartmentId { get; set; }
    [JsonIgnore]
    public Department? Department { get; set; }
}
