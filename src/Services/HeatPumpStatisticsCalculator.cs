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
        public StatisticsResult Calculate(IList<HeatPumpDatum> heatPumpData, IList<HeatPumpDataPerPeriod> heatPumpDataPerPeriods, DateTime now){

            var result = new StatisticsResult(null,null);
            Console.WriteLine("Calculate statistics!");
            
            var latestDailyStatistic = DateTime.MinValue;
            if(heatPumpDataPerPeriods.Any()){
                latestDailyStatistic = heatPumpDataPerPeriods.Where(h => h.PeriodKind == "Day").Select(h => h.DateCreated).Max();
            }
            var lastDay = heatPumpData.Where(hpd => hpd.DateCreated >= latestDailyStatistic).ToList();
            var oldestRecordInRecent1Day = lastDay.Select(hpd => hpd.DateCreated).Min();
            var dailyStatisticsContainer = GetEmptyDailyStatisticsContainer(heatPumpData, oldestRecordInRecent1Day, now);            
            Console.WriteLine($"Calculate daily statistics: latest stats: {latestDailyStatistic.ToShortDateString()}; oldest Measurement: {oldestRecordInRecent1Day}");

            var latestWeeklyStatistic = DateTime.MinValue;
            if(heatPumpDataPerPeriods.Any()){
                latestWeeklyStatistic = heatPumpDataPerPeriods.Where(h => h.PeriodKind == "Week").Select(h => h.DateCreated).Max();
            }
            var lastWeek = heatPumpData.Where(hpd => hpd.DateCreated >= latestWeeklyStatistic).ToList();
            var oldestRecordInRecentWeek = lastWeek.Select(hpd => hpd.DateCreated).Min();
            var weeklyStatisticsContainer = GetEmptyWeeklyStatisticsContainer(heatPumpData, oldestRecordInRecentWeek, now);
            Console.WriteLine($"Calculate weekly statistics: latest stats: {latestWeeklyStatistic.ToShortDateString()}; oldest Measurement: {oldestRecordInRecentWeek}");

            var latestMonthlyStatistic = DateTime.MinValue;
            if(heatPumpDataPerPeriods.Any()){
                latestMonthlyStatistic = heatPumpDataPerPeriods.Where(h => h.PeriodKind == "Month").Select(h => h.DateCreated).Max();
            }
            var lastMonth = heatPumpData.Where(hpd => hpd.DateCreated >= latestMonthlyStatistic).ToList();
            var oldestRecordInRecentMonth = lastMonth.Select(hpd => hpd.DateCreated).Min();
            var monthlyStatisticsContainer = GetEmptyMonthlyStatisticsContainer(heatPumpData, oldestRecordInRecentMonth, now);   
            Console.WriteLine($"Calculate monthly statistics: latest Stats: {latestMonthlyStatistic.ToShortDateString()}; oldest Measurement: {oldestRecordInRecentMonth}");         
            
            var latestYearlyStatistic = DateTime.MinValue;
            if(heatPumpDataPerPeriods.Any()){
                heatPumpDataPerPeriods.Where(h => h.PeriodKind == "Year").Select(h => h.DateCreated).Max();
            }
            var lastYear = heatPumpData.Where(hpd => hpd.DateCreated >= latestYearlyStatistic).ToList();
            var oldestRecordInRecent366Days = lastYear.Select(hpd => hpd.DateCreated).Min();
            var yearlyStatisticsContainer = GetEmptyYearlyStatisticsContainer(heatPumpData, oldestRecordInRecent366Days, now);
            Console.WriteLine($"Calculate yearly statistics: latest stats: {latestYearlyStatistic.ToShortDateString()}; oldest Measurement: {oldestRecordInRecent366Days}");

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

        private IList<PeriodStatistics> GetEmptyDailyStatisticsContainer(IList<HeatPumpDatum> heatPumpData, DateTime oldestRecord, DateTime now) 
            => GetEmptyPeriodStatisticsContainer(heatPumpData, oldestRecord, now, PeriodKind.Day, c => c.AddDays(1));

        private IList<PeriodStatistics> GetEmptyWeeklyStatisticsContainer(IList<HeatPumpDatum> heatPumpData, DateTime oldestRecord, DateTime now) 
        {
            var periodStatistics = new List<PeriodStatistics>();
            var startOfPeriod = oldestRecord;
            while(startOfPeriod.DayOfWeek != DayOfWeek.Monday && startOfPeriod < now){
                startOfPeriod = startOfPeriod.AddDays(1);
            }
            while(startOfPeriod < now){
                var endOfPeriod = startOfPeriod.AddDays(7);
                var dataSetsInPeriod = heatPumpData.Where(h => h.DateCreated >= startOfPeriod && h.DateCreated < endOfPeriod);
                if(dataSetsInPeriod.Any()){
                    periodStatistics.Add(new PeriodStatistics(
                        startOfPeriod.Year, 
                        startOfPeriod.WeekOfYear(new CultureInfo("de-DE")), 
                        PeriodKind.Week,
                        startOfPeriod.Date, 
                        startOfPeriod.AddDays(7), 
                        null));
                }
                startOfPeriod = endOfPeriod;
            }
            return periodStatistics;
        }

        private IList<PeriodStatistics> GetEmptyMonthlyStatisticsContainer(IList<HeatPumpDatum> heatPumpData, DateTime oldestRecord, DateTime now) 
        {
            var periodStatistics = new List<PeriodStatistics>();
            var startOfPeriod = oldestRecord;
            // Find start of month
            while(startOfPeriod.Day != 1 && startOfPeriod < now){
                startOfPeriod = startOfPeriod.AddDays(1);
            }
            while(startOfPeriod < now){
                var endOfPeriod = startOfPeriod.AddMonths(1);
                var dataSetsInPeriod = heatPumpData.Where(h => h.DateCreated >= startOfPeriod && h.DateCreated < endOfPeriod);
                if(dataSetsInPeriod.Any()){
                    periodStatistics.Add(new PeriodStatistics(
                        startOfPeriod.Year, 
                        startOfPeriod.Day, 
                        PeriodKind.Month,
                        startOfPeriod.Date, 
                        startOfPeriod.AddMonths(1), 
                        null));
                }
                startOfPeriod = endOfPeriod;
            }
            return periodStatistics;
        }

        private IList<PeriodStatistics> GetEmptyYearlyStatisticsContainer(IList<HeatPumpDatum> heatPumpData, DateTime oldestRecord, DateTime now) 
        {
                var periodStatistics = new List<PeriodStatistics>();
            var startOfPeriod = oldestRecord;
            while(startOfPeriod.Day != 1 && startOfPeriod.Month != 1 && startOfPeriod < now){
                startOfPeriod = startOfPeriod.AddDays(1);
            }
            while(startOfPeriod < now){
                var endOfPeriod = startOfPeriod.AddYears(1);
                var dataSetsInPeriod = heatPumpData.Where(h => h.DateCreated >= startOfPeriod && h.DateCreated < endOfPeriod);
                if(dataSetsInPeriod.Any()){
                    periodStatistics.Add(new PeriodStatistics(
                        startOfPeriod.Year, 
                        startOfPeriod.Year, 
                        PeriodKind.Year,
                        startOfPeriod.Date, 
                        endOfPeriod, 
                        null));
                }
                startOfPeriod = endOfPeriod;
            }
            return periodStatistics;
        }    

        private IList<PeriodStatistics> GetEmptyPeriodStatisticsContainer(IList<HeatPumpDatum> heatPumpData, DateTime oldestRecord, DateTime now, PeriodKind periodKind, Func<DateTime, DateTime> increaseByPeriod)
        {
            var periodStatistics = new List<PeriodStatistics>();
            var startOfPeriod = oldestRecord;
            while(startOfPeriod < now){
                var endOfPeriod = increaseByPeriod(startOfPeriod);
                var dataSetsInPeriod = heatPumpData.Where(h => h.DateCreated >= startOfPeriod && h.DateCreated < endOfPeriod);
                if(dataSetsInPeriod.Any()){
                    periodStatistics.Add(new PeriodStatistics(
                        startOfPeriod.Year, 
                        startOfPeriod.DayOfYear, 
                        PeriodKind.Day,
                        startOfPeriod.Date, 
                        endOfPeriod, 
                        null));
                }
                startOfPeriod = endOfPeriod;
            }
            return periodStatistics;
        }
    }
}