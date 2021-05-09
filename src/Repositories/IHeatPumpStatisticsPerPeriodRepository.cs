using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpStatisticsPerPeriodRepository
    {
        HeatPumpDataPerPeriod FindByYearAndPeriodNumber(int year, int periodNumber);
        List<HeatPumpDataPerPeriod> GetRecentSevenDays(DateTime now);
        List<HeatPumpDataPerPeriod> GetRecentTwelveWeeks(DateTime now, CultureInfo cultureInfo);
        List<HeatPumpDataPerPeriod> GetRecentTwelveMonths(DateTime now);
        List<HeatPumpDataPerPeriod> GetAllRecordsFromRecent366Days(DateTime now);
        List<HeatPumpDataPerPeriod> GetYearlyRecords(DateTime now);
        void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod);
        void Update(HeatPumpDataPerPeriod heatPumpDataPerPeriod);
    }
}