using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Repositories;

namespace StiebelEltronApiServer.Services {
    public class HeatPumpStatisticsCalculatorJob : CronJobService {
        private readonly IHeatPumpStatisticsCalculator _heatPumpStatisticsCalculator;
        private readonly ILogger<HeatPumpStatisticsCalculatorJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HeatPumpStatisticsCalculatorJob (IScheduleConfig<HeatPumpStatisticsCalculatorJob> config,
            IHeatPumpStatisticsCalculator heatPumpStatisticsCalculator,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<HeatPumpStatisticsCalculatorJob> logger) : base (config.CronExpression, config.TimeZoneInfo) {
            _heatPumpStatisticsCalculator = heatPumpStatisticsCalculator;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public override Task StartAsync (CancellationToken cancellationToken) {
            _logger.LogInformation ("HeatPumpStatisticsCalculatorJob starts.");
            return base.StartAsync (cancellationToken);
        }

        public override async Task DoWork (CancellationToken cancellationToken) {
            try {
                _logger.LogInformation ($"{DateTime.Now:hh:mm:ss} HeatPumpStatisticsCalculatorJob is working.");
                using var scope = _serviceScopeFactory.CreateScope ();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork> ();
                var recordsOfRecentYear = unitOfWork.HeatPumpDataRepository.GetLastYear ();
                var twelveMonthsPeriodAggregations = unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentTwelveMonths(DateTime.Now);
                var statisticsResult = _heatPumpStatisticsCalculator.Calculate (recordsOfRecentYear,twelveMonthsPeriodAggregations, DateTime.Now);
                if(statisticsResult.DataSetsToRemove.Any()){
                    unitOfWork.HeatPumpDataRepository.RemoveRange(statisticsResult.DataSetsToRemove);
                }

                foreach (var stats in statisticsResult.Statistics) {
                    var statsInDb = unitOfWork.HeatPumpStatisticsPerPeriodRepository.FindByYearAndPeriodNumber ((int) stats.Year, stats.PeriodNumber);
                    if (statsInDb != null) {
                        unitOfWork.HeatPumpStatisticsPerPeriodRepository.Update (statsInDb.UpdateWith(stats));
                    }
                    unitOfWork.HeatPumpStatisticsPerPeriodRepository.Add (stats);
                }
                _logger.LogInformation ($"{DateTime.Now:hh:mm:ss} HeatPumpStatisticsCalculatorJob saving changes.");
                var changes = await unitOfWork.SaveChanges ();
                _logger.LogInformation ($"{DateTime.Now:hh:mm:ss} HeatPumpStatisticsCalculatorJob saved {changes} changed database rows.");
            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
            }

        }

        public override Task StopAsync (CancellationToken cancellationToken) {
            _logger.LogInformation ("HeatPumpStatisticsCalculatorJob is stopping.");
            return base.StopAsync (cancellationToken);
        }
    }
}