using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Repositories;

namespace StiebelEltronDashboard.Services
{
    public class HeatPumpStatisticsCalculatorJob : CronJobService
    {
        private readonly IHeatPumpStatisticsCalculator _heatPumpStatisticsCalculator;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HeatPumpStatisticsCalculatorJob(IScheduleConfig<HeatPumpStatisticsCalculatorJob> config,
            IHeatPumpStatisticsCalculator heatPumpStatisticsCalculator,
            IServiceScopeFactory serviceScopeFactory,
            ILogger logger) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _heatPumpStatisticsCalculator = heatPumpStatisticsCalculator;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("HeatPumpStatisticsCalculatorJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                var formatString = "yyyyMMddHHmmss";
                _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob is working.");
                using var scope = _serviceScopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                var recordsOfRecentYear = unitOfWork.HeatPumpDataRepository.GetLastYear();
                var allRecordsFromRecent366Days = unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetAllRecordsFromRecent366Days(DateTime.Now);
                var statisticsResult = _heatPumpStatisticsCalculator.Calculate(recordsOfRecentYear, allRecordsFromRecent366Days, DateTime.Now);
                foreach (var newStatisticsResult in statisticsResult.Statistics)
                {
                    _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob add/update {newStatisticsResult.First}-{newStatisticsResult.Last}.");
                    var existingStatisticsResults = unitOfWork.HeatPumpStatisticsPerPeriodRepository.FindByYearPeriodKindAndPeriodNumber((int)newStatisticsResult.Year, newStatisticsResult.PeriodKind, newStatisticsResult.PeriodNumber);
                    _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob existingStatisticsResults {existingStatisticsResults.Count()}.");
                    if (existingStatisticsResults.Any() && existingStatisticsResults.Count() > 1)
                    {
                        var latestMatchInExistingStatistics = existingStatisticsResults.Where(y => y.DateUpdated.CompareTo(existingStatisticsResults.Max(s => s.DateUpdated)) == 0).FirstOrDefault();
                        _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob latest {latestMatchInExistingStatistics?.DateUpdated}.");
                        foreach (var statisticInDatabase in existingStatisticsResults)
                        {
                            if (statisticInDatabase.DateUpdated.CompareTo(latestMatchInExistingStatistics.DateUpdated) != 0)
                            {
                                _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob remove {statisticInDatabase.First}-{statisticInDatabase.Last}.");
                                unitOfWork.HeatPumpStatisticsPerPeriodRepository.Remove(statisticInDatabase);
                            }
                        }
                        if (existingStatisticsResults != null)
                        {
                            _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob update {newStatisticsResult.First}-{newStatisticsResult.Last}.");
                            unitOfWork.HeatPumpStatisticsPerPeriodRepository.Update(latestMatchInExistingStatistics.UpdateWith(newStatisticsResult));
                        }
                    }
                    _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob add {newStatisticsResult.First}-{newStatisticsResult.Last}.");
                    unitOfWork.HeatPumpStatisticsPerPeriodRepository.Add(newStatisticsResult);
                }
                var changes = await unitOfWork.SaveChanges();
                _logger.Information($"{DateTime.Now.ToString(formatString)} HeatPumpStatisticsCalculatorJob saved {changes} changed database rows.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information("HeatPumpStatisticsCalculatorJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}