using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.WorkerProductions;
public sealed class WorkerWeeklyProduction : Entity
{
    public Guid WorkerProductionId { get; set; }
    public WorkerProduction? WorkerProduction { get; set; }

    public int? WeeklyActual { get; set; }
    public int? WeeklyTarget { get; set; }
    public double? WeeklyYield { get; set; }

    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}
