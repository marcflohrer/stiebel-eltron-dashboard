using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StiebelEltronApiServer.Repositories
{
    public class HeatPumpStatisticsPerPeriodRepository : IHeatPumpStatisticsPerPeriodRepository {
        private readonly ApplicationDbContext _applicationDbContext;
         
        public HeatPumpStatisticsPerPeriodRepository (ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public HeatPumpDataPerPeriod FindByYearAndPeriodNumber(int year, int periodNumber) 
            => _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp => hpdpp.Year == year 
                && hpdpp.PeriodNumber == periodNumber).FirstOrDefault();

        public void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod) 
            => _applicationDbContext.HeatPumpDataPerPeriods.Add(heatPumpDataPerPeriod);

        public void Update(HeatPumpDataPerPeriod heatPumpDataPerPeriod) 
            => _applicationDbContext.HeatPumpDataPerPeriods.Update(heatPumpDataPerPeriod);

        public List<HeatPumpDataPerPeriod> GetRecentSevenDays(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(7));
            var firstDay = startOfRequestedPeriod.DayOfYear;
            var year = startOfRequestedPeriod.Year;
            return  _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp 
                => hpdpp.Year == year 
                    && hpdpp.PeriodKind == "Day" 
                    && hpdpp.PeriodNumber >= firstDay
                    && hpdpp.First != DateTime.MaxValue)
                    .ToList();
        }

        public List<HeatPumpDataPerPeriod> GetRecentTwelveWeeks(DateTime now, CultureInfo cultureInfo)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(12 * 7));
            var firstWeekNumberOfRequestedPeriod = startOfRequestedPeriod.WeekOfYear(cultureInfo);
            var year = startOfRequestedPeriod.Year;
            return _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp 
                => hpdpp.Year == year
                    && hpdpp.PeriodKind == "Week" 
                    && hpdpp.PeriodNumber >= firstWeekNumberOfRequestedPeriod)
                    .ToList();
        }

        public List<HeatPumpDataPerPeriod> GetRecentTwelveMonths(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(365));
            var year = startOfRequestedPeriod.Year;
            var firstMonthOfRequestedPeriod = startOfRequestedPeriod.Month;
            return _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp 
                => hpdpp.Year == year 
                    && hpdpp.PeriodKind == "Month" 
                    && hpdpp.PeriodNumber >= firstMonthOfRequestedPeriod)
                    .ToList();
        }

        public List<HeatPumpDataPerPeriod> GetYearlyRecords(DateTime now)
        {
            return _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp 
                => hpdpp.Year == now.Year 
                    && hpdpp.PeriodKind == "Year")
                    .ToList();
        }

        public List<HeatPumpDataPerPeriod> GetAllRecordsFromRecent366Days(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(365));
            var year = startOfRequestedPeriod.Year;
            var firstMonthOfRequestedPeriod = startOfRequestedPeriod.Month;
            return _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp 
                => hpdpp.DateCreated >= startOfRequestedPeriod).ToList();
        }
    }
}