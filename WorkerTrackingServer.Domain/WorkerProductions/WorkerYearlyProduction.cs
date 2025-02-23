using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.WorkerProductions;
public sealed class WorkerYearlyProduction : Entity
{
    public Guid WorkerProductionId { get; set; }
    public WorkerProduction? WorkerProduction { get; set; }

    public int? YearlyActual { get; set; }
    public int? YearlyTarget { get; set; }
    public double? YearlyYield { get; set; }

    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}
