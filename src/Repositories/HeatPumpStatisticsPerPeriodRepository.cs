using Microsoft.EntityFrameworkCore;
using Serilog;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Repositories
{
    public class HeatPumpStatisticsPerPeriodRepository : IHeatPumpStatisticsPerPeriodRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public HeatPumpStatisticsPerPeriodRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public List<HeatPumpDataPerPeriod> FindByYearPeriodKindAndPeriodNumber(int year, string periodKind, int periodNumber)
            => applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp => hpdpp.Year == year
                && hpdpp.PeriodKind == periodKind
                && hpdpp.PeriodNumber == periodNumber).ToList();

        public void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod)
            => applicationDbContext.HeatPumpDataPerPeriods.Add(heatPumpDataPerPeriod);

        public void Update(HeatPumpDataPerPeriod heatPumpDataPerPeriod)
            => applicationDbContext.HeatPumpDataPerPeriods.Update(heatPumpDataPerPeriod);

        public List<HeatPumpDataPerPeriod> GetRecentDays(DateTime now)
        {
            var numberOfDaysRequesting = 24;
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(numberOfDaysRequesting));
            var firstDay = startOfRequestedPeriod.DayOfYear;
            var yearOfFirstDay = startOfRequestedPeriod.Year;
            var resultInYearOfFirstDay = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year == yearOfFirstDay
                    && hpdpp.PeriodKind == "Day"
                    && hpdpp.PeriodNumber >= firstDay
                    && hpdpp.First != DateTime.MaxValue);
            var resultList = resultInYearOfFirstDay.OrderBy(x => x.Last)
                       .MyDistinctBy(x => x.PeriodNumber)
                       .Take(numberOfDaysRequesting).ToList();

            var endOfRequestedPeriod = now;
            var lastDay = endOfRequestedPeriod.DayOfYear;
            var yearOfLastDay = endOfRequestedPeriod.Year;
            if (yearOfFirstDay != yearOfLastDay)
            {
                var listInNewYear = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                    => hpdpp.Year == yearOfLastDay
                        && hpdpp.PeriodKind == "Day"
                        && hpdpp.PeriodNumber < lastDay);

                Log.Debug("Number of days found in old year: " + resultList.Count());
                Log.Debug("Number of days found in new year: " + listInNewYear.Count());

                resultList.AddRange(listInNewYear.ToList());

                Log.Debug("Number of days found in both years: " + resultList.Count());
            }

            return resultList;
        }

        private static List<HeatPumpDataPerPeriod> GetDistinctPeriodNumber(IQueryable<HeatPumpDataPerPeriod> list, List<HeatPumpDataPerPeriod> result, HeatPumpDataPerPeriod previous)
        {
            var orderedList = list.OrderBy(e => e.PeriodNumber);
            foreach (var elem in orderedList)
            {
                if (previous == null)
                {
                    result.Add(elem);
                    previous = elem;
                    Log.Debug($"Previous: {elem.PeriodKind}: {elem.PeriodNumber} {elem.First} {elem.Year}");
                }
                else if (previous.PeriodNumber < elem.PeriodNumber)
                {
                    result.Add(elem);
                    previous = elem;
                    Log.Debug($" Add: {elem.PeriodKind}: {elem.PeriodNumber} {elem.First} {elem.Year}");
                }
            }

            return result;
        }

        public List<HeatPumpDataPerPeriod> GetRecentWeeks(DateTime now, CultureInfo cultureInfo)
        {
            var numberOfWeekRequesting = 24;
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(numberOfWeekRequesting * 7));
            var firstWeekNumberOfRequestedPeriod = startOfRequestedPeriod.WeekOfYear(cultureInfo);
            var yearOfFirstWeek = startOfRequestedPeriod.Year;
            var listForYearOfFirstWeekNumber = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year >= yearOfFirstWeek
                    && hpdpp.PeriodKind == "Week"
                    && hpdpp.PeriodNumber >= firstWeekNumberOfRequestedPeriod);
            var result = new List<HeatPumpDataPerPeriod>();
            HeatPumpDataPerPeriod previous = null;
            result = GetDistinctPeriodNumber(listForYearOfFirstWeekNumber, result, previous);
            var resultList = result.OrderBy(x => x.Last)
                         .MyDistinctBy(x => x.PeriodNumber)
                         .Take(numberOfWeekRequesting).ToList();

            var endOfRequestedPeriod = now;
            var lastWeekNumberOfRequestedPeriod = endOfRequestedPeriod.WeekOfYear(cultureInfo);
            var yearOfLastWeek = endOfRequestedPeriod.Year;
            if (yearOfFirstWeek != yearOfLastWeek)
            {
                var listInNewYear = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year == yearOfLastWeek
                    && hpdpp.PeriodKind == "Week"
                    && hpdpp.PeriodNumber <= lastWeekNumberOfRequestedPeriod);

                Log.Debug("Number of weeks found in old year: " + resultList.Count());
                Log.Debug("Number of weeks found in new year: " + listInNewYear.Count());

                resultList.AddRange(listInNewYear);

                Log.Debug("Number of weeks found in both years: " + resultList.Count());
            }
            return resultList;
        }

        public List<HeatPumpDataPerPeriod> GetRecentMonths(DateTime now)
        {
            var numberOfMonthsRequesting = 24;
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(numberOfMonthsRequesting * 31));
            var yearOfFirstMonth = startOfRequestedPeriod.Year;
            var firstMonthOfRequestedPeriod = startOfRequestedPeriod.Month;
            var list = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year >= yearOfFirstMonth && hpdpp.PeriodKind == "Month");
            var result = new List<HeatPumpDataPerPeriod>();
            HeatPumpDataPerPeriod previous = null;
            result = GetDistinctPeriodNumber(list, result, previous);
            var resultList = result.OrderBy(x => x.Last)
                         .MyDistinctBy(x => x.PeriodNumber)
                         .Take(numberOfMonthsRequesting)
                         .ToList();

            var endOfRequestedPeriod = now;
            var lastMonthOfRequestedPeriod = endOfRequestedPeriod.Month;
            var yearOfLastMonth = endOfRequestedPeriod.Year;
            if (yearOfFirstMonth != yearOfLastMonth)
            {
                var listInNewYear = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year == yearOfLastMonth
                    && hpdpp.PeriodKind == "Month"
                    && hpdpp.PeriodNumber <= lastMonthOfRequestedPeriod);

                Log.Debug("Number of months found in old year: " + resultList.Count());
                Log.Debug("Number of months found in new year: " + listInNewYear.Count());

                resultList.AddRange(listInNewYear);

                Log.Debug("Number of months found in both years: " + resultList.Count());
            }

            return resultList;
        }

        public List<HeatPumpDataPerPeriod> GetYearlyRecords(DateTime now)
        {
            var numberOfYearsRequesting = 24;
            var list = applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.PeriodKind == "Year");
            var result = new List<HeatPumpDataPerPeriod>();
            HeatPumpDataPerPeriod previous = null;
            result = GetDistinctPeriodNumber(list, result, previous);
            return result.OrderBy(x => x.Last)
                         .MyDistinctBy(x => x.PeriodNumber)
                         .Take(numberOfYearsRequesting).ToList();
        }

        public List<HeatPumpDataPerPeriod> GetAllRecordsFromRecent366Days(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(365));
            var year = startOfRequestedPeriod.Year;
            var firstMonthOfRequestedPeriod = startOfRequestedPeriod.Month;
            return applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.DateCreated >= startOfRequestedPeriod).ToList();
        }

        public void Remove(HeatPumpDataPerPeriod heatPumpDataPerPeriod)
        {
            applicationDbContext.HeatPumpDataPerPeriods.Remove(heatPumpDataPerPeriod);
        }

        public async Task<List<HeatPumpDataPerPeriod>> GetRecordsWithoutPeriodStartEndAsync(int chunkSize)
            => await applicationDbContext.HeatPumpDataPerPeriods
                .Where(hpd => (hpd.PeriodStart.Year == DateTime.MinValue.Year
                && hpd.PeriodStart.DayOfYear == DateTime.MinValue.DayOfYear)
                        || (hpd.PeriodEnd.Year == DateTime.MinValue.Year
                && hpd.PeriodEnd.DayOfYear == DateTime.MinValue.DayOfYear))
                .OrderBy(hpd => hpd.Year)
                .OrderBy(hpd => hpd.PeriodKind)
                .OrderBy(hpd => hpd.PeriodNumber)
                .Take(chunkSize)
                .ToListAsync();

        public async Task<List<HeatPumpDataPerPeriod>> AllAsync()
            => await applicationDbContext.HeatPumpDataPerPeriods.ToListAsync();
    }
}
