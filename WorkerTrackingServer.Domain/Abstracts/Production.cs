using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.Abstracts;
public class Production : Entity
{
    public int? DailyTarget { get; set; }
    public int? WeeklyTarget { get; set; }
    public int? MonthlyTarget { get; set; }
    public int? YearlyTarget { get; set; }

    public int? DailyActual { get; set; }
    public int? WeeklyActual { get; set; }
    public int? MonthlyActual { get; set; }
    public int? YearlyActual { get; set; }

    public double? DailyYield { get; set; }
    public double? WeeklyYield { get; set; }
    public double? MonthlyYield { get; set; }
    public double? YearlyYield { get; set; }
}
