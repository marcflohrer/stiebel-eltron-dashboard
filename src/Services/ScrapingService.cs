using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
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
        }

        public async Task<HeatPumpDatum> GetHeatPumpInformationAsync () {
            return await GetHeatPumpInformationAsync (string.Empty);
        }

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
                TotalPowerConsumption = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.TotalPowerConsumption),
                    ActualSpeedDensifier = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ActualSpeedDensifier),
                    AntiFreezeTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.AntiFreezeTemperature),
                    CompressorInletTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.CompressorInletTemperature),
                    CondenserTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.CondenserTemperature),
                    DefrostStarts = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.DefrostStarts),
                    DefrostTime = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.DefrostTime),
                    EvaporatorTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.EvaporatorTemperature),
                    ExhaustAirTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ExhaustAirTemperature),
                    FanPowerRel = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.FanPowerRel),
                    HighPressure = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.HighPressure),
                    HotGasTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.HotGasTemperature),
                    InletTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.InletTemperature),
                    IntermediateInjectionTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.IntermediateInjectionTemperature),
                    LowPressure = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.LowPressure),
                    OilSumpTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.OilSumpTemperature),
                    OutdoorTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.OutdoorTemperature),
                    PowerConsumptionHeatingDay = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.PowerConsumptionHeatingDay),
                    PowerConsumptionHeatingSum = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.PowerConsumptionHeatingSum),
                    PowerConsumptionHotWaterDay = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.PowerConsumptionHotWaterDay),
                    PowerConsumptionHotWaterSum = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.PowerConsumptionHotWaterSum),
                    PressureMedium = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.PressureMedium),
                    ReheatingStages1 = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ReheatingStages1),
                    ReheatingStages2 = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ReheatingStages2),
                    ReheatingStagesHeatQuantityHeatingSum = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ReheatingStagesHeatQuantityHeatingSum),
                    ReheatingStagesHeatQuantityHotWaterTotal = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ReheatingStagesHeatQuantityHotWaterTotal),
                    ReturnTemperature = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.ReturnTemperature),
                    RuntimeVaporizerDefrost = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.RuntimeVaporizerDefrost),
                    RuntimeVaporizerHeating = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.RuntimeVaporizerHeating),
                    RuntimeVaporizerHotWater = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.RuntimeVaporizerHotWater),
                    SettingSpeedCompressed = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.SettingSpeedCompressed),
                    VaporizerHeatQuantityHeatingDay = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.VaporizerHeatQuantityHeatingDay),
                    VaporizerHeatQuantityHeatingTotal = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.VaporizerHeatQuantityHeatingTotal),
                    VaporizerHeatQuantityHotWaterDay = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.VaporizerHeatQuantityHotWaterDay),
                    VaporizerHeatQuantityHotWaterTotal = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.VaporizerHeatQuantityHotWaterTotal),
                    VoltageInverter = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.VoltageInverter),
                    WaterVolumeCurrent = _websiteParser.GetValueFromSite (htmlDocument, ScrapingValue.WaterVolumeCurrent),
                    DateCreated = utcNow,
                    DateUpdated = utcNow
            };
        }
    }
}