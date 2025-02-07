using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.Departments;
public class Department : Entity
{
    public string DepartmentName { get; set; } = string.Empty;
}
