using StiebelEltronDashboard.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace StiebelEltronDashboard.Extensions;

public static class HeatPumpDataPerPeriodListExtensions
{
    public static string ToJson(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriod)
        => JsonConvert.SerializeObject(heatPumpDataPerPeriod);
    public static IList<HeatPumpDataPerPeriod> GetDays(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriods)
        => heatPumpDataPerPeriods.Where(x => x.PeriodKind == "Day").ToList();
    public static IList<HeatPumpDataPerPeriod> GetWeeks(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriods)
        => heatPumpDataPerPeriods.Where(x => x.PeriodKind == "Week").ToList();
    public static IList<HeatPumpDataPerPeriod> GetMonths(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriods)
        => heatPumpDataPerPeriods.Where(x => x.PeriodKind == "Month").OrderBy(m => m.First.Date.Year * 100 + m.First.Date.Month).ToList();
    public static IList<HeatPumpDataPerPeriod> GetYears(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriods)
        => heatPumpDataPerPeriods.Where(x => x.PeriodKind == "Year").ToList();
}