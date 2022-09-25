using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Repositories;
using StiebelEltronDashboard.Services.HtmlServices;

namespace StiebelEltronDashboard.Services
{
    public class CollectHeatPumpDataJob : CronJobService
    {
        private readonly IServiceWeltService _serviceWeltService;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CollectHeatPumpDataJob(IScheduleConfig<CollectHeatPumpDataJob> config,
            IServiceWeltService scrapingService,
            IServiceScopeFactory serviceScopeFactory,
            ILogger logger) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _serviceWeltService = scrapingService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("CollectHeatPumpDataJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                _logger.Information($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob is working.");
                var language = await ReadCurrentIsgLanguage();
                if (language.Name != "DEUTSCH")
                {
                    await _serviceWeltService.SetLanguageAsync("DEUTSCH", language.sessionId);
                    var currentLanguage = await ReadCurrentIsgLanguage();
                    _logger.Information($"{DateTime.Now:hh:mm:ss} Setting language temporarily to {currentLanguage.Name}.");
                }
                var heatPumpData = await _serviceWeltService.GetHeatPumpInformationAsync();
                using var scope = _serviceScopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                unitOfWork.HeatPumpDataRepository.InsertHeatPumpData(heatPumpData);
                _logger.Information($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob saving changes.");
                var changes = await unitOfWork.SaveChanges();
                _logger.Information($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob saved {changes} changed database rows.");
                if (language.Name != "DEUTSCH")
                {
                    await _serviceWeltService.SetLanguageAsync(language.Name, language.sessionId);
                    var currentLanguage = await ReadCurrentIsgLanguage();
                    _logger.Information($"{DateTime.Now:hh:mm:ss} Reset language back to {currentLanguage.Name}.");
                }
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
            }

        }

        private async Task<ServiceWeltService.LanguageName> ReadCurrentIsgLanguage()
        {
            var language = await _serviceWeltService.GetCurrentLanguageSettingAsync();
            _logger.Information($"{DateTime.Now:hh:mm:ss} Current ISG Language ${language.Name}");
            return language;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information("CollectHeatPumpDataJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}