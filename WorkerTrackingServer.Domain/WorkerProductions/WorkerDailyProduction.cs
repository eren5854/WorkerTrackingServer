using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.WorkerProductions;
public sealed class WorkerDailyProduction : Entity
{
    public Guid WorkerProductionId { get; set; }
    public WorkerProduction? WorkerProduction { get; set; }

    public int? DailyActual { get; set; }
    public int? DailyTarget { get; set; }
    public double? DailyYield { get; set; }

    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}
