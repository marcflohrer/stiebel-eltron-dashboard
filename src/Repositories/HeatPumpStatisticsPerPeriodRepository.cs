using Serilog;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StiebelEltronDashboard.Repositories
{
    public class HeatPumpStatisticsPerPeriodRepository : IHeatPumpStatisticsPerPeriodRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HeatPumpStatisticsPerPeriodRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<HeatPumpDataPerPeriod> FindByYearPeriodKindAndPeriodNumber(int year, string periodKind, int periodNumber)
            => _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp => hpdpp.Year == year
                && hpdpp.PeriodKind == periodKind
                && hpdpp.PeriodNumber == periodNumber).ToList();

        public void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod)
            => _applicationDbContext.HeatPumpDataPerPeriods.Add(heatPumpDataPerPeriod);

        public void Update(HeatPumpDataPerPeriod heatPumpDataPerPeriod)
            => _applicationDbContext.HeatPumpDataPerPeriods.Update(heatPumpDataPerPeriod);

        public List<HeatPumpDataPerPeriod> GetRecentSevenDays(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(7));
            var firstDay = startOfRequestedPeriod.DayOfYear;
            var yearOfFirstDay = startOfRequestedPeriod.Year;
            var resultInYearOfFirstDay = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year == yearOfFirstDay
                    && hpdpp.PeriodKind == "Day"
                    && hpdpp.PeriodNumber >= firstDay
                    && hpdpp.First != DateTime.MaxValue);
            var resultList = resultInYearOfFirstDay.OrderBy(x => x.Last)
                       .MyDistinctBy(x => x.PeriodNumber)
                       .Take(7).ToList();

            var endOfRequestedPeriod = now;
            var lastDay = endOfRequestedPeriod.DayOfYear;
            var yearOfLastDay = endOfRequestedPeriod.Year;
            if (yearOfFirstDay != yearOfLastDay)
            {
                var listInNewYear = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
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

        public List<HeatPumpDataPerPeriod> GetRecentTwelveWeeks(DateTime now, CultureInfo cultureInfo)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(12 * 7));
            var firstWeekNumberOfRequestedPeriod = startOfRequestedPeriod.WeekOfYear(cultureInfo);
            var yearOfFirstWeek = startOfRequestedPeriod.Year;
            var listForYearOfFirstWeekNumber = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year >= yearOfFirstWeek
                    && hpdpp.PeriodKind == "Week"
                    && hpdpp.PeriodNumber >= firstWeekNumberOfRequestedPeriod);
            var result = new List<HeatPumpDataPerPeriod>();
            HeatPumpDataPerPeriod previous = null;
            result = GetDistinctPeriodNumber(listForYearOfFirstWeekNumber, result, previous);
            var resultList = result.OrderBy(x => x.Last)
                         .MyDistinctBy(x => x.PeriodNumber)
                         .Take(12).ToList();

            var endOfRequestedPeriod = now;
            var lastWeekNumberOfRequestedPeriod = endOfRequestedPeriod.WeekOfYear(cultureInfo);
            var yearOfLastWeek = endOfRequestedPeriod.Year;
            if (yearOfFirstWeek != yearOfLastWeek)
            {
                var listInNewYear = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
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

        public List<HeatPumpDataPerPeriod> GetRecentTwelveMonths(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(365));
            var yearOfFirstMonth = startOfRequestedPeriod.Year;
            var firstMonthOfRequestedPeriod = startOfRequestedPeriod.Month;
            var list = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.Year >= yearOfFirstMonth && hpdpp.PeriodKind == "Month");
            var result = new List<HeatPumpDataPerPeriod>();
            HeatPumpDataPerPeriod previous = null;
            result = GetDistinctPeriodNumber(list, result, previous);
            var resultList = result.OrderBy(x => x.Last)
                         .MyDistinctBy(x => x.PeriodNumber)
                         .Take(12)
                         .ToList();

            var endOfRequestedPeriod = now;
            var lastMonthOfRequestedPeriod = endOfRequestedPeriod.Month;
            var yearOfLastMonth = endOfRequestedPeriod.Year;
            if (yearOfFirstMonth != yearOfLastMonth)
            {
                var listInNewYear = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
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
            var list = _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.PeriodKind == "Year");
            var result = new List<HeatPumpDataPerPeriod>();
            HeatPumpDataPerPeriod previous = null;
            result = GetDistinctPeriodNumber(list, result, previous);
            return result.OrderBy(x => x.Last)
                         .MyDistinctBy(x => x.PeriodNumber)
                         .Take(12).ToList();
        }

        public List<HeatPumpDataPerPeriod> GetAllRecordsFromRecent366Days(DateTime now)
        {
            var startOfRequestedPeriod = now.Subtract(TimeSpan.FromDays(365));
            var year = startOfRequestedPeriod.Year;
            var firstMonthOfRequestedPeriod = startOfRequestedPeriod.Month;
            return _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp
                => hpdpp.DateCreated >= startOfRequestedPeriod).ToList();
        }

        public void Remove(HeatPumpDataPerPeriod heatPumpDataPerPeriod)
        {
            _applicationDbContext.HeatPumpDataPerPeriods.Remove(heatPumpDataPerPeriod);
        }
    }
}