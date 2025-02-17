using WorkerTrackingServer.Domain.Abstract;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Domain.Departments;
public sealed class Department : Entity
{
    public string DepartmentName { get; set; } = string.Empty;
    public string? DepartmentDescription { get; set; }

    //public List<Worker>? Workers { get; set; }
    //public List<DepartmentProduction>? DepartmentProductions { get; set; }
}
