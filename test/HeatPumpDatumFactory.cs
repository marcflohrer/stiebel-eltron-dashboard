using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;

namespace StiebelEltronApiServerTests
{
    public class HeatPumpDatumFactory
    {
        public static IEnumerable<HeatPumpDatum> Create(DateTime start, int numberOfDataSets, Func<int, DateTime, DateTime> incrementTime)
        {
            for(var i = 0; i < numberOfDataSets; ++i){
                yield return new HeatPumpDatum ().SetDoubles (i).SetDateTimes (incrementTime(i, start));
            }
        }
    }
}