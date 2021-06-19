using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Repositories;

namespace StiebelEltronDashboard.Services
{
    public class DeleteOldHeatPumpStatisticsJob : CronJobService {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DeleteOldHeatPumpStatisticsJob (IScheduleConfig<HeatPumpStatisticsCalculatorJob> config,
            IServiceScopeFactory serviceScopeFactory,
            ILogger logger) : base (config.CronExpression, config.TimeZoneInfo) {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public override Task StartAsync (CancellationToken cancellationToken) {
            _logger.Information ("DeleteOldHeatPumpStatisticsJob starts.");
            return base.StartAsync (cancellationToken);
        }

        public override async Task DoWork (CancellationToken cancellationToken) {
            try {
                _logger.Information ($"{DateTime.Now:hh:mm:ss} DeleteOldHeatPumpStatisticsJob is working.");
                using var scope = _serviceScopeFactory.CreateScope ();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork> ();
                var recordsOfRecentYear = unitOfWork.HeatPumpDataRepository.GetLastYear ();
                var allRecordsOlderThan1Year = unitOfWork.HeatPumpDataRepository.GetAllRecordsOlderThan366Days (DateTime.Now); 
                unitOfWork.HeatPumpDataRepository.RemoveRange (allRecordsOlderThan1Year);
                var changes = await unitOfWork.SaveChanges ();
                _logger.Information ($"{DateTime.Now:hh:mm:ss} HeatPumpStatisticsCalculatorJob saved {changes} changed database rows.");

            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
            }
        }

        public override Task StopAsync (CancellationToken cancellationToken) {
            _logger.Information ("DeleteOldHeatPumpStatisticsJob is stopping.");
            return base.StopAsync (cancellationToken);
        }
    }
}