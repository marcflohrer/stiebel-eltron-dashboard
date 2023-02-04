using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Models;
using StiebelEltronDashboard.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StiebelEltronDashboardTests
{
    public class HeatPumpDataPerPeriodFactory
    {
        public static IEnumerable<HeatPumpDataPerPeriod> Create(DateTime start, int numberOfDataSets, Func<int, DateTime, DateTime> incrementTime)
        {
            var heatPumpDataPerPeriod = new HeatPumpDataPerPeriod();
            heatPumpDataPerPeriod.Year = start.Year;
            var now = new DateTime(2021, 5, 1);
            var result = new List<HeatPumpDataPerPeriod>();
            var index = 0;
            for (var i = 0; i < numberOfDataSets; ++i)
            {
                var date = incrementTime(i, start);
                if (i % 4 == 0)
                {
                    var dayOfYear = date.DayOfYear;
                    result.Add(new HeatPumpDataPerPeriod()
                        .SetMinDoubles(index)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(index)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(index)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(index)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(index)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(0)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Day.ToString())
                        .SetPeriodNumber(dayOfYear)
                        .SetFirst(date)
                        .SetLast(date)
                        .SetDateTimes<HeatPumpDataPerPeriod>(date)
                        .SetDateCreated(now)
                        .SetDateUpdated(now)
                        .SetPeriodStart(PeriodDateProvider.GetPeriodStart(date.Year, PeriodKind.Day, dayOfYear))
                        .SetPeriodEnd(PeriodDateProvider.GetPeriodEnd(date.Year, PeriodKind.Day, dayOfYear))
                        );
                    index++;
                }
            }
            var weekNumber = start.WeekOfYear(new CultureInfo("de-DE"));
            result.Add(new HeatPumpDataPerPeriod()
                        .SetMinDoubles(0)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(0)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(0)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(0)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(0)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(0)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Week.ToString())
                        .SetPeriodNumber(weekNumber)
                        .SetDateTimes<HeatPumpDataPerPeriod>(incrementTime(++numberOfDataSets, start))
                        .SetFirst(start)
                        .SetLast(start)
                        .SetDateCreated(now)
                        .SetDateUpdated(now)
                        .SetPeriodStart(PeriodDateProvider.GetPeriodStart(start.Year, PeriodKind.Week, weekNumber))
                        .SetPeriodEnd(PeriodDateProvider.GetPeriodEnd(start.Year, PeriodKind.Week, weekNumber))
                        );

            var firstRecord = start.AddDays(4);
            weekNumber = firstRecord.WeekOfYear(new CultureInfo("de-DE"));
            result.Add(new HeatPumpDataPerPeriod()
                        .SetMinDoubles(1)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(2)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(1.5)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(1)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(2)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(1)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Week.ToString())
                        .SetPeriodNumber(weekNumber)
                        .SetDateTimes<HeatPumpDataPerPeriod>(incrementTime(++numberOfDataSets, start))
                        .SetFirst(firstRecord)
                        .SetLast(start.AddDays(8))
                        .SetDateCreated(now)
                        .SetDateUpdated(now)
                        .SetPeriodStart(PeriodDateProvider.GetPeriodStart(start.Year, PeriodKind.Week, weekNumber))
                        .SetPeriodEnd(PeriodDateProvider.GetPeriodEnd(start.Year, PeriodKind.Week, weekNumber))
                        );

            firstRecord = start.AddDays(12);
            weekNumber = firstRecord.WeekOfYear(new CultureInfo("de-DE"));
            result.Add(new HeatPumpDataPerPeriod()
                        .SetMinDoubles(3)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(4)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(3.5)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(3)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(4)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(1)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Week.ToString())
                        .SetPeriodNumber(weekNumber)
                        .SetDateTimes<HeatPumpDataPerPeriod>(firstRecord)
                        .SetFirst(firstRecord)
                        .SetLast(start.AddDays(16))
                        .SetDateCreated(now)
                        .SetDateUpdated(now)
                        .SetPeriodStart(PeriodDateProvider.GetPeriodStart(start.Year, PeriodKind.Week, weekNumber))
                        .SetPeriodEnd(PeriodDateProvider.GetPeriodEnd(start.Year, PeriodKind.Week, weekNumber))
                        );
            result.Add(new HeatPumpDataPerPeriod()
                        .SetMinDoubles(0)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(3)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(1.5)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(0)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(3)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(3)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Month.ToString())
                        .SetPeriodNumber(start.Month)
                        .SetDateTimes<HeatPumpDataPerPeriod>(firstRecord)
                        .SetFirst(firstRecord.Subtract(TimeSpan.FromDays(12)))
                        .SetLast(start.AddDays(12))
                        .SetDateCreated(now)
                        .SetDateUpdated(now)
                        .SetPeriodStart(PeriodDateProvider.GetPeriodStart(start.Year, PeriodKind.Month, start.Month))
                        .SetPeriodEnd(PeriodDateProvider.GetPeriodEnd(start.Year, PeriodKind.Month, start.Month))
                        );
            result.Add(new HeatPumpDataPerPeriod()
                        .SetMinDoubles(0)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(15)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(7.5)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(0)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(15)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(15)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Year.ToString())
                        .SetPeriodNumber(start.Year)
                        .SetDateTimes<HeatPumpDataPerPeriod>(firstRecord)
                        .SetFirst(firstRecord.Subtract(TimeSpan.FromDays(12)))
                        .SetLast(start.AddDays(60))
                        .SetDateCreated(now)
                        .SetDateUpdated(now)
                        .SetPeriodStart(PeriodDateProvider.GetPeriodStart(start.Year, PeriodKind.Year, start.Year))
                        .SetPeriodEnd(PeriodDateProvider.GetPeriodEnd(start.Year, PeriodKind.Year, start.Year))
                        );

            return result;
        }
    }
}