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

    public int? DailyYield { get; set; }
    public int? WeeklyYield { get; set; }
    public int? MonthlyYield { get; set; }
    public int? YearlyYield { get; set; }
}
