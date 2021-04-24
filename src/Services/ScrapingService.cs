using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Services {
    public class ScrapingService : IScrapingService {
        private readonly IServiceWeltFacade _serviceWeltFacade;
        private readonly ITidyUpDirtyHtml _tidyUpDirtyHtml;
        private readonly IXpathService _xpathService;
        private string _sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891";

        public ScrapingService (IServiceWeltFacade serviceWeltFacade,
            ITidyUpDirtyHtml tidyUpDirtyHtml,
            IXpathService xpathService) {
            _serviceWeltFacade = serviceWeltFacade;
            _tidyUpDirtyHtml = tidyUpDirtyHtml;
            _xpathService = xpathService;
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
            var totalPowerConsumption = _xpathService.GetValueFor (htmlDocument, ScrapingValue.TotalPowerConsumption);

            if (totalPowerConsumption == null) {
                throw new Exception("Could not request servicewelt url.");
            } else {
                var totalPowerConsumptionDouble = 0d;
                totalPowerConsumption = totalPowerConsumption.Trim().Replace(',', '.');
                if (totalPowerConsumption.EndsWith ("MWh")) {
                    totalPowerConsumption = totalPowerConsumption.Replace ("MWh", string.Empty);
                    totalPowerConsumptionDouble = Double.Parse (totalPowerConsumption) * Math.Pow (10, 6);
                } else if (totalPowerConsumption.EndsWith ("kWh")) {
                    totalPowerConsumption = totalPowerConsumption.Replace ("kWh", string.Empty);
                    totalPowerConsumptionDouble = Double.Parse (totalPowerConsumption);
                    totalPowerConsumptionDouble = Double.Parse (totalPowerConsumption) * Math.Pow (10, 3);
                } else {
                    throw new Exception ("Unknown unit: " + totalPowerConsumption);
                }
                return new HeatPumpDatum {
                    TotalPowerConsumption = totalPowerConsumptionDouble
                };
            }
            throw new Exception ($"Login failed for user! ould not request servicewelt url. {totalPowerConsumption}");
        }
    }
}