using StiebelEltronDashboard.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StiebelEltronDashboard.Repositories
{
    public interface IHeatPumpStatisticsPerPeriodRepository
    {
        List<HeatPumpDataPerPeriod> FindByYearPeriodKindAndPeriodNumber(int year, string periodKind, int periodNumber);
        List<HeatPumpDataPerPeriod> GetRecentSevenDays(DateTime now);
        List<HeatPumpDataPerPeriod> GetRecentTwelveWeeks(DateTime now, CultureInfo cultureInfo);
        List<HeatPumpDataPerPeriod> GetRecentTwelveMonths(DateTime now);
        List<HeatPumpDataPerPeriod> GetAllRecordsFromRecent366Days(DateTime now);
        List<HeatPumpDataPerPeriod> GetYearlyRecords(DateTime now);
        void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod);
        void Update(HeatPumpDataPerPeriod heatPumpDataPerPeriod);
        void Remove(HeatPumpDataPerPeriod heatPumpDataPerPeriod);
    }
}