using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class ScrapingService : IScrapingService
    {
        private readonly IServiceWeltFacade _serviceWeltFacade;
        private readonly ITidyUpDirtyHtml _tidyUpDirtyHtml;
        private string _sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891";

        public ScrapingService(IServiceWeltFacade serviceWeltFacade,
            ITidyUpDirtyHtml tidyUpDirtyHtml,
            IWebsiteParser websiteParser)
        {
            _serviceWeltFacade = serviceWeltFacade;
            _tidyUpDirtyHtml = tidyUpDirtyHtml;
            HtmlDocumentExtensions.WebsiteParser = websiteParser;
        }

        public async Task<HeatPumpDatum> GetHeatPumpInformationAsync() => await GetHeatPumpInformationAsync(string.Empty);

        public async Task<HeatPumpDatum> GetHeatPumpInformationAsync(string sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891")
        {
            if (!string.IsNullOrEmpty(sessionId) && string.IsNullOrEmpty(_sessionId))
            {
                _sessionId = sessionId;
            }
            else if (!string.IsNullOrEmpty(_sessionId) && string.IsNullOrEmpty(sessionId))
            {
                sessionId = _sessionId;
            }
            var htmlDocument = new HtmlDocument();
            var serviceWelt = await _serviceWeltFacade.GetHeatPumpWebsiteAsync(sessionId);
            var tidyHtml = _tidyUpDirtyHtml.GetTidyHtml(serviceWelt.HtmlDocument);
            htmlDocument.LoadHtml(tidyHtml);

            var documentNode = htmlDocument.DocumentNode;
            var outerHtml = documentNode.OuterHtml;
            htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(outerHtml);
            var utcNow = DateTime.UtcNow;
            return new HeatPumpDatum
            {
                TotalPowerConsumption = htmlDocument.ParseFor(Metric.TotalPowerConsumption),
                ActualSpeedDensifier = htmlDocument.ParseFor(Metric.ActualSpeedDensifier),
                AntiFreezeTemperature = htmlDocument.ParseFor(Metric.AntiFreezeTemperature),
                CompressorInletTemperature = htmlDocument.ParseFor(Metric.CompressorInletTemperature),
                CondenserTemperature = htmlDocument.ParseFor(Metric.CondenserTemperature),
                DefrostStarts = htmlDocument.ParseFor(Metric.DefrostStarts),
                DefrostTime = htmlDocument.ParseFor(Metric.DefrostTime),
                EvaporatorTemperature = htmlDocument.ParseFor(Metric.EvaporatorTemperature),
                ExhaustAirTemperature = htmlDocument.ParseFor(Metric.ExhaustAirTemperature),
                FanPowerRel = htmlDocument.ParseFor(Metric.FanPowerRel),
                HighPressure = htmlDocument.ParseFor(Metric.HighPressure),
                HotGasTemperature = htmlDocument.ParseFor(Metric.HotGasTemperature),
                InletTemperature = htmlDocument.ParseFor(Metric.InletTemperature),
                IntermediateInjectionTemperature = htmlDocument.ParseFor(Metric.IntermediateInjectionTemperature),
                LowPressure = htmlDocument.ParseFor(Metric.LowPressure),
                OilSumpTemperature = htmlDocument.ParseFor(Metric.OilSumpTemperature),
                OutdoorTemperature = htmlDocument.ParseFor(Metric.OutdoorTemperature),
                PowerConsumptionHeatingDay = htmlDocument.ParseFor(Metric.PowerConsumptionHeatingDay),
                PowerConsumptionHeatingSum = htmlDocument.ParseFor(Metric.PowerConsumptionHeatingSum),
                PowerConsumptionHotWaterDay = htmlDocument.ParseFor(Metric.PowerConsumptionHotWaterDay),
                PowerConsumptionHotWaterSum = htmlDocument.ParseFor(Metric.PowerConsumptionHotWaterSum),
                PressureMedium = htmlDocument.ParseFor(Metric.PressureMedium),
                ReheatingStages1 = htmlDocument.ParseFor(Metric.ReheatingStages1),
                ReheatingStages2 = htmlDocument.ParseFor(Metric.ReheatingStages2),
                ReheatingStagesHeatQuantityHeatingSum = htmlDocument.ParseFor(Metric.ReheatingStagesHeatQuantityHeatingSum),
                ReheatingStagesHeatQuantityHotWaterTotal = htmlDocument.ParseFor(Metric.ReheatingStagesHeatQuantityHotWaterTotal),
                ReturnTemperature = htmlDocument.ParseFor(Metric.ReturnTemperature),
                RuntimeVaporizerDefrost = htmlDocument.ParseFor(Metric.RuntimeVaporizerDefrost),
                RuntimeVaporizerHeating = htmlDocument.ParseFor(Metric.RuntimeVaporizerHeating),
                RuntimeVaporizerHotWater = htmlDocument.ParseFor(Metric.RuntimeVaporizerHotWater),
                SettingSpeedCompressed = htmlDocument.ParseFor(Metric.SettingSpeedCompressed),
                VaporizerHeatQuantityHeatingDay = htmlDocument.ParseFor(Metric.VaporizerHeatQuantityHeatingDay),
                VaporizerHeatQuantityHeatingTotal = htmlDocument.ParseFor(Metric.VaporizerHeatQuantityHeatingTotal),
                VaporizerHeatQuantityHotWaterDay = htmlDocument.ParseFor(Metric.VaporizerHeatQuantityHotWaterDay),
                VaporizerHeatQuantityHotWaterTotal = htmlDocument.ParseFor(Metric.VaporizerHeatQuantityHotWaterTotal),
                VoltageInverter = htmlDocument.ParseFor(Metric.VoltageInverter),
                WaterVolumeCurrent = htmlDocument.ParseFor(Metric.WaterVolumeCurrent),
                DateCreated = utcNow,
                DateUpdated = utcNow
            };
        }
    }
}