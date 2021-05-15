using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Repositories;
using StiebelEltronDashboard.Services.HtmlServices;

namespace StiebelEltronDashboard.Services
{
    public class CollectHeatPumpDataJob : CronJobService
    {
        private readonly IScrapingService _scrapingService;
        private readonly ILogger<CollectHeatPumpDataJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CollectHeatPumpDataJob(IScheduleConfig<CollectHeatPumpDataJob> config,
            IScrapingService scrapingService,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<CollectHeatPumpDataJob> logger) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _scrapingService = scrapingService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CollectHeatPumpDataJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob is working.");
                var heatPumpData = await _scrapingService.GetHeatPumpInformationAsync();
                using var scope = _serviceScopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                unitOfWork.HeatPumpDataRepository.InsertHeatPumpData(heatPumpData);
                _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob saving changes.");
                var changes = await unitOfWork.SaveChanges();
                _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob saved {changes} changed database rows.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CollectHeatPumpDataJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}