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
        private readonly ServiceWeltService.LanguageName ParsingLanguage =
            new ServiceWeltService.LanguageName(Name: "DEUTSCH", string.Empty);

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
                var isgLanguage = await ReadCurrentIsgLanguage();

                // Parsing only works when language setting is german
                await SetLanguage(isgLanguage, ParsingLanguage);

                var scope = await ParseAndStoreHeatPumpMetrics();

                // Reset language to the one found when entering the function.
                await SetLanguage(ParsingLanguage, isgLanguage);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
            }

        }

        private async Task<IServiceScope> ParseAndStoreHeatPumpMetrics()
        {
            var heatPumpData = await _serviceWeltService.GetHeatPumpInformationAsync();
            using var scope = _serviceScopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
            unitOfWork.HeatPumpDataRepository.InsertHeatPumpData(heatPumpData);
            _logger.Information($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob saving changes.");
            var changes = await unitOfWork.SaveChanges();
            _logger.Information($"{DateTime.Now:hh:mm:ss} CollectHeatPumpDataJob saved {changes} changed database rows.");
            return scope;
        }

        private async Task<ServiceWeltService.LanguageName>
            SetLanguage(ServiceWeltService.LanguageName currentLanguage,
            ServiceWeltService.LanguageName targetLanguage)
        {
            if (currentLanguage.Name == targetLanguage.Name)
            {
                _logger.Debug($"Changing languages not needed.");
                return currentLanguage;
            }
            var tempLanguage = currentLanguage;
            var sessionId = currentLanguage.sessionId;
            while (tempLanguage.Name != targetLanguage.Name)
            {
                await _serviceWeltService.SetLanguageAsync(targetLanguage.Name, sessionId);
                tempLanguage = await ReadCurrentIsgLanguage();
                if (tempLanguage.Name != targetLanguage.Name)
                {
                    Thread.Sleep(5000);
                    _logger.Warning($"Setting language to {targetLanguage.Name} failed.");
                }
            }

            return tempLanguage;
        }

        private async Task<ServiceWeltService.LanguageName> ReadCurrentIsgLanguage()
        {
            var language = await _serviceWeltService.GetCurrentLanguageSettingAsync();
            while (string.IsNullOrWhiteSpace(language.Name))
            {
                _logger.Warning($"Reading language failed. Retry in 5 seconds.");
                Thread.Sleep(5000);
                language = await _serviceWeltService.GetCurrentLanguageSettingAsync();
            }
            _logger.Information($"{DateTime.Now:hh:mm:ss} Current ISG Language {language.Name}");
            return language;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information("CollectHeatPumpDataJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}