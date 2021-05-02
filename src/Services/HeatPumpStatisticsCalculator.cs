using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StiebelEltronApiServer.Services
{
    public class HeatPumpStatisticsCalculator : IHeatPumpStatisticsCalculator
    {
        private readonly IStatisticsService _statisticsService;

        public HeatPumpStatisticsCalculator(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        public StatisticsResult Calculate(IList<HeatPumpDatum> heatPumpData, DateTime now){
            var result = new StatisticsResult(null,null);
            var lastYear = heatPumpData.Where(hpd => hpd.DateCreated >= now.Subtract(TimeSpan.FromDays(366))).ToList();
            var oldestRecordInRecent366Days = lastYear.Select(hpd => hpd.DateCreated).Min();
            var dailyStatisticsContainer = GetEmptyDailyStatisticsContainer(oldestRecordInRecent366Days, now);            
            var weeklyStatisticsContainer = GetEmptyWeeklyStatisticsContainer(oldestRecordInRecent366Days, now);
            var monthlyStatisticsContainer = GetEmptyMonthlyStatisticsContainer(oldestRecordInRecent366Days, now);
            var yearlyStatisticsContainer = GetEmptyYearlyStatisticsContainer(oldestRecordInRecent366Days, now);
            
            var statistics = new List<HeatPumpDataPerPeriod>();
            statistics.AddRange(GetDailyStatistics(heatPumpData, dailyStatisticsContainer, now));
            statistics.AddRange(GetWeeklyStatistics(heatPumpData, weeklyStatisticsContainer, now));
            statistics.AddRange(GetMonthlyStatistics(heatPumpData, monthlyStatisticsContainer, now));
            statistics.AddRange(GetYearlyStatistics(heatPumpData, yearlyStatisticsContainer, now));

            var dataSetsToRemove = heatPumpData.Where(hpd => hpd.DateCreated < now.Subtract(TimeSpan.FromDays(366))).ToList();
            return new StatisticsResult(dataSetsToRemove, statistics);
        }

        private IList<HeatPumpDataPerPeriod> GetDailyStatistics(IList<HeatPumpDatum> heatPumpData, IList<PeriodStatistics> containers, DateTime now) 
            => GetPeriodStatistics(heatPumpData, containers, PeriodKind.Day, now);

        private IList<HeatPumpDataPerPeriod> GetWeeklyStatistics(IList<HeatPumpDatum> heatPumpData, IList<PeriodStatistics> containers, DateTime now) 
            => GetPeriodStatistics(heatPumpData, containers, PeriodKind.Week, now);

        private IList<HeatPumpDataPerPeriod> GetMonthlyStatistics(IList<HeatPumpDatum> heatPumpData, IList<PeriodStatistics> containers, DateTime now) 
            => GetPeriodStatistics(heatPumpData, containers, PeriodKind.Month, now); 

        private IList<HeatPumpDataPerPeriod> GetYearlyStatistics(IList<HeatPumpDatum> heatPumpData, IList<PeriodStatistics> containers, DateTime now) 
            => GetPeriodStatistics(heatPumpData, containers, PeriodKind.Year, now); 

        private IList<HeatPumpDataPerPeriod> GetPeriodStatistics(IList<HeatPumpDatum> heatPumpData, IList<PeriodStatistics> containers, PeriodKind periodKind, DateTime now)
        {
            var result = new List<HeatPumpDataPerPeriod>();
            foreach(var container in containers){
                var dataSetsInPeriod = heatPumpData.Where(h => h.DateCreated >= container.Start && h.DateCreated < container.End);
                var periodResult = _statisticsService.GetHeatPumpDataPerPeriod(dataSetsInPeriod, container.Start.Year, periodKind.ToString(), container.PeriodNumber, now);
                result.Add(periodResult);
            }
            return result;
        }

        private IList<PeriodStatistics> GetEmptyDailyStatisticsContainer(DateTime oldestRecord, DateTime now) 
            => GetEmptyPeriodStatisticsContainer(oldestRecord, now, PeriodKind.Day, c => c.AddDays(1));

        private IList<PeriodStatistics> GetEmptyWeeklyStatisticsContainer(DateTime oldestRecord, DateTime now) 
        {
            var periodStatistics = new List<PeriodStatistics>();
            var current = oldestRecord;
            while(current.DayOfWeek != DayOfWeek.Monday && current < now){
                current = current.AddDays(1);
            }
            while(current < now){
                periodStatistics.Add(new PeriodStatistics(
                    current.Year, 
                    current.WeekOfYear(new CultureInfo("de-DE")), 
                    PeriodKind.Week,
                    current.Date, 
                    current.AddDays(7), 
                    null));
                current = (current.AddDays(7));
            }
            return periodStatistics;
        }

        private IList<PeriodStatistics> GetEmptyMonthlyStatisticsContainer(DateTime oldestRecord, DateTime now) 
        {
            var periodStatistics = new List<PeriodStatistics>();
            var current = oldestRecord;
            while(current.Day != 1 && current < now){
                current = current.AddDays(1);
            }
            while(current < now){
                periodStatistics.Add(new PeriodStatistics(
                    current.Year, 
                    current.Day, 
                    PeriodKind.Month,
                    current.Date, 
                    current.AddMonths(1), 
                    null));
                current = (current.AddMonths(1));
            }
            return periodStatistics;
        }

        private IList<PeriodStatistics> GetEmptyYearlyStatisticsContainer(DateTime oldestRecord, DateTime now) 
        {
                var periodStatistics = new List<PeriodStatistics>();
            var current = oldestRecord;
            while(current.Day != 1 && current.Month != 1 && current < now){
                current = current.AddDays(1);
            }
            while(current < now){
                periodStatistics.Add(new PeriodStatistics(
                    current.Year, 
                    current.Year, 
                    PeriodKind.Year,
                    current.Date, 
                    current.AddYears(1), 
                    null));
                current = (current.AddYears(1));
            }
            return periodStatistics;
        }    

        private IList<PeriodStatistics> GetEmptyPeriodStatisticsContainer(DateTime oldestRecord, DateTime now, PeriodKind periodKind, Func<DateTime, DateTime> increaseByPeriod)
        {
            var periodStatistics = new List<PeriodStatistics>();
            var current = oldestRecord;
            while(current < now){
                periodStatistics.Add(new PeriodStatistics(
                    current.Year, 
                    current.DayOfYear, 
                    PeriodKind.Day,
                    current.Date, 
                    increaseByPeriod(current), 
                    null));
                current = increaseByPeriod(current);
            }
            return periodStatistics;
        }
    }
}