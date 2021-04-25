using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Services {
    public class ScrapingService : IScrapingService {
        private readonly IServiceWeltFacade _serviceWeltFacade;
        private readonly ITidyUpDirtyHtml _tidyUpDirtyHtml;
        private readonly IWebsiteParser _websiteParser;
        private string _sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891";

        public ScrapingService (IServiceWeltFacade serviceWeltFacade,
            ITidyUpDirtyHtml tidyUpDirtyHtml,
            IWebsiteParser websiteParser) {
            _serviceWeltFacade = serviceWeltFacade;
            _tidyUpDirtyHtml = tidyUpDirtyHtml;
            _websiteParser = websiteParser;
            HtmlDocumentExtensions.WebsiteParser = _websiteParser;
        }

        public async Task<HeatPumpDatum> GetHeatPumpInformationAsync() => await GetHeatPumpInformationAsync(string.Empty);

        public double GetValueFrom(HtmlDocument htmlDocument, ScrapingValue value) => _websiteParser.GetValueFromSite(htmlDocument, value);

        public async Task<HeatPumpDatum> GetHeatPumpInformationAsync (string sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891") {
            if (!string.IsNullOrEmpty (sessionId) && string.IsNullOrEmpty (_sessionId)) {
                _sessionId = sessionId;
            } else if (!string.IsNullOrEmpty (_sessionId) && string.IsNullOrEmpty (sessionId)) {
                sessionId = _sessionId;
            }
            var htmlDocument = new HtmlDocument ();
            var serviceWelt = await _serviceWeltFacade.GetHeatPumpWebsiteAsync (sessionId);
            var tidyHtml = _tidyUpDirtyHtml.GetTidyHtml (serviceWelt.HtmlDocument);
            htmlDocument.LoadHtml (tidyHtml);

            var documentNode = htmlDocument.DocumentNode;
            var outerHtml = documentNode.OuterHtml;
            htmlDocument = new HtmlDocument ();
            htmlDocument.LoadHtml (outerHtml);
            var utcNow = DateTime.UtcNow;
            return new HeatPumpDatum {
                TotalPowerConsumption = htmlDocument.ParseFor(ScrapingValue.TotalPowerConsumption),
                    ActualSpeedDensifier = htmlDocument.ParseFor(ScrapingValue.ActualSpeedDensifier),
                    AntiFreezeTemperature = htmlDocument.ParseFor(ScrapingValue.AntiFreezeTemperature),
                    CompressorInletTemperature = htmlDocument.ParseFor(ScrapingValue.CompressorInletTemperature),
                    CondenserTemperature = htmlDocument.ParseFor(ScrapingValue.CondenserTemperature),
                    DefrostStarts = htmlDocument.ParseFor(ScrapingValue.DefrostStarts),
                    DefrostTime = htmlDocument.ParseFor(ScrapingValue.DefrostTime),
                    EvaporatorTemperature = htmlDocument.ParseFor(ScrapingValue.EvaporatorTemperature),
                    ExhaustAirTemperature = htmlDocument.ParseFor(ScrapingValue.ExhaustAirTemperature),
                    FanPowerRel = htmlDocument.ParseFor(ScrapingValue.FanPowerRel),
                    HighPressure = htmlDocument.ParseFor(ScrapingValue.HighPressure),
                    HotGasTemperature = htmlDocument.ParseFor(ScrapingValue.HotGasTemperature),
                    InletTemperature = htmlDocument.ParseFor(ScrapingValue.InletTemperature),
                    IntermediateInjectionTemperature = htmlDocument.ParseFor(ScrapingValue.IntermediateInjectionTemperature),
                    LowPressure = htmlDocument.ParseFor(ScrapingValue.LowPressure),
                    OilSumpTemperature = htmlDocument.ParseFor(ScrapingValue.OilSumpTemperature),
                    OutdoorTemperature = htmlDocument.ParseFor(ScrapingValue.OutdoorTemperature),
                    PowerConsumptionHeatingDay = htmlDocument.ParseFor(ScrapingValue.PowerConsumptionHeatingDay),
                    PowerConsumptionHeatingSum = htmlDocument.ParseFor(ScrapingValue.PowerConsumptionHeatingSum),
                    PowerConsumptionHotWaterDay = htmlDocument.ParseFor(ScrapingValue.PowerConsumptionHotWaterDay),
                    PowerConsumptionHotWaterSum = htmlDocument.ParseFor(ScrapingValue.PowerConsumptionHotWaterSum),
                    PressureMedium = htmlDocument.ParseFor(ScrapingValue.PressureMedium),
                    ReheatingStages1 = htmlDocument.ParseFor(ScrapingValue.ReheatingStages1),
                    ReheatingStages2 = htmlDocument.ParseFor(ScrapingValue.ReheatingStages2),
                    ReheatingStagesHeatQuantityHeatingSum = htmlDocument.ParseFor(ScrapingValue.ReheatingStagesHeatQuantityHeatingSum),
                    ReheatingStagesHeatQuantityHotWaterTotal = htmlDocument.ParseFor(ScrapingValue.ReheatingStagesHeatQuantityHotWaterTotal),
                    ReturnTemperature = htmlDocument.ParseFor(ScrapingValue.ReturnTemperature),
                    RuntimeVaporizerDefrost = htmlDocument.ParseFor(ScrapingValue.RuntimeVaporizerDefrost),
                    RuntimeVaporizerHeating = htmlDocument.ParseFor(ScrapingValue.RuntimeVaporizerHeating),
                    RuntimeVaporizerHotWater = htmlDocument.ParseFor(ScrapingValue.RuntimeVaporizerHotWater),
                    SettingSpeedCompressed = htmlDocument.ParseFor(ScrapingValue.SettingSpeedCompressed),
                    VaporizerHeatQuantityHeatingDay = htmlDocument.ParseFor(ScrapingValue.VaporizerHeatQuantityHeatingDay),
                    VaporizerHeatQuantityHeatingTotal = htmlDocument.ParseFor(ScrapingValue.VaporizerHeatQuantityHeatingTotal),
                    VaporizerHeatQuantityHotWaterDay = htmlDocument.ParseFor(ScrapingValue.VaporizerHeatQuantityHotWaterDay),
                    VaporizerHeatQuantityHotWaterTotal = htmlDocument.ParseFor(ScrapingValue.VaporizerHeatQuantityHotWaterTotal),
                    VoltageInverter = htmlDocument.ParseFor(ScrapingValue.VoltageInverter),
                    WaterVolumeCurrent = htmlDocument.ParseFor(ScrapingValue.WaterVolumeCurrent),
                    DateCreated = utcNow,
                    DateUpdated = utcNow
            };
        }
    }
}