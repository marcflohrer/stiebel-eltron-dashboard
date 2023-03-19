using StiebelEltronDashboard.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System;
using Serilog;

namespace StiebelEltronDashboard.Extensions;

public static class HeatPumpDataPerPeriodListExtensions
{
    public static string ToJson(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriod)
        => JsonConvert.SerializeObject(heatPumpDataPerPeriod);

    public static IList<HeatPumpDataPerPeriod> GetTimeUnit(this IEnumerable<HeatPumpDataPerPeriod> heatPumpDataPerPeriods, string timeUnit)
    {
        var distinctCriteria = GetDistinctCriteria(timeUnit);
        var foundDuplicates = heatPumpDataPerPeriods.Where(x => x.PeriodKind == timeUnit)
            .GroupBy(x => distinctCriteria(x))
            .Where(g => g.Count() > 1)
            .Any();
        if (foundDuplicates)
        {
            Log.Warning("More than one data record for time unit - before filtering");
        }

        var result = heatPumpDataPerPeriods.Where(x => x.PeriodKind == timeUnit)
            .DistinctBy(x => distinctCriteria(x))
            .ToList();
        foundDuplicates = result
            .GroupBy(x => distinctCriteria(x))
            .Where(g => g.Count() > 1)
            .Any();
        if (foundDuplicates)
        {
            Log.Error("More than one data record for time unit - after filtering");
        }
        return result;
    }

    private static Func<HeatPumpDataPerPeriod, string> GetDistinctCriteria(string timeUnit) => timeUnit switch
    {
        "Day" => GetDayOfYearString(),
        "Week" => GetWeekNumberString(),
        "Month" => GetMonthNumberString(),
        "Year" => GetYearNumberString(),
        _ => throw new InvalidProgramException($"Unknown timeunit {timeUnit}")
    };

    public static Func<HeatPumpDataPerPeriod, DateTime> GetDayOfYear() => x => new DateTime((int)x.Year, 1, 1).Add(TimeSpan.FromDays(x.PeriodNumber - 1));
    public static Func<HeatPumpDataPerPeriod, DateTime> GetYearNumber() => x => new DateTime((int)x.Year, 1, 1);
    public static Func<HeatPumpDataPerPeriod, DateTime> GetMonthNumber() => x => new DateTime((int)x.Year, x.PeriodNumber, 1);
    public static Func<HeatPumpDataPerPeriod, DateTime> GetWeekNumber() => x => new DateTime((int)x.Year, 1, 1).FirstDateOfWeek((int)x.PeriodNumber, new System.Globalization.CultureInfo("de-DE"));

    private static Func<HeatPumpDataPerPeriod, string> GetDayOfYearString() => x => GetDayOfYear().Invoke(x).ToString("s");
    private static Func<HeatPumpDataPerPeriod, string> GetYearNumberString() => x => GetYearNumber().Invoke(x).ToString("s");
    private static Func<HeatPumpDataPerPeriod, string> GetMonthNumberString() => x => GetMonthNumber().Invoke(x).ToString("s");
    private static Func<HeatPumpDataPerPeriod, string> GetWeekNumberString() => x => GetWeekNumber().Invoke(x).ToString("s");
}
