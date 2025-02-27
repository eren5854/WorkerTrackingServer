using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.WorkerProductions;
public sealed class WorkerMonthlyProduction : Entity
{
    [JsonIgnore]
    public Guid WorkerProductionId { get; set; }
    [JsonIgnore]
    public WorkerProduction? WorkerProduction { get; set; }

    public int? MonthlyActual { get; set; }
    public int? MonthlyTarget { get; set; }
    public double? MonthlyYield { get; set; }

    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}
