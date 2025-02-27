using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.WorkerProductions;
public sealed class WorkerYearlyProduction : Entity
{
    [JsonIgnore]
    public Guid WorkerProductionId { get; set; }
    [JsonIgnore]
    public WorkerProduction? WorkerProduction { get; set; }

    public int? YearlyActual { get; set; }
    public int? YearlyTarget { get; set; }
    public double? YearlyYield { get; set; }

    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}
