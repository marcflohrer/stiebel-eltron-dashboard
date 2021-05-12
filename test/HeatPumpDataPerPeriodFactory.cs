using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StiebelEltronApiServerTests
{
    public class HeatPumpDataPerPeriodFactory
    {
        public static IEnumerable<HeatPumpDataPerPeriod> Create(DateTime start, int numberOfDataSets, Func<int, DateTime, DateTime> incrementTime)
        {
            var heatPumpDataPerPeriod = new HeatPumpDataPerPeriod ();
            heatPumpDataPerPeriod.Year = start.Year;
            var result = new List<HeatPumpDataPerPeriod>();
            var index = 0;
            for(var i = 0; i < numberOfDataSets; ++i){
                var date = incrementTime(i, start);
                if(i % 4 == 0)
                {
                    result.Add(new HeatPumpDataPerPeriod ()
                        .SetMinDoubles (index)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(index)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(index)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(index)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(index)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(0)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Day.ToString())
                        .SetPeriodNumber(date.DayOfYear)
                        .SetFirst(date)
                        .SetLast(date)
                        .SetDateTimes<HeatPumpDataPerPeriod>(date));
                        index++;
                }                   
            }
            var firstRecord = start.AddDays(4);
            result.Add(new HeatPumpDataPerPeriod ()
                        .SetMinDoubles (1)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(2)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(1.5)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(1)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(2)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(1)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Week.ToString())
                        .SetPeriodNumber(firstRecord.WeekOfYear(new CultureInfo("de-DE")))
                        .SetDateTimes<HeatPumpDataPerPeriod>(incrementTime(++numberOfDataSets, start))
                        .SetFirst(firstRecord)
                        .SetLast(start.AddDays(8)));
            firstRecord = start.AddDays(12); 
            result.Add(new HeatPumpDataPerPeriod ()
                        .SetMinDoubles (3)
                        .SetMaxDoubles<HeatPumpDataPerPeriod>(4)
                        .SetAverageDoubles<HeatPumpDataPerPeriod>(3.5)
                        .SetStartDoubles<HeatPumpDataPerPeriod>(3)
                        .SetEndDoubles<HeatPumpDataPerPeriod>(4)
                        .SetDeltaDoubles<HeatPumpDataPerPeriod>(1)
                        .SetYear(start.Year)
                        .SetPeriodKind(PeriodKind.Week.ToString())
                        .SetPeriodNumber(firstRecord.WeekOfYear(new CultureInfo("de-DE")))
                        .SetDateTimes<HeatPumpDataPerPeriod>(firstRecord)
                        .SetFirst(firstRecord)
                        .SetLast(start.AddDays(16)));
            return result;                           
        }
    }
}