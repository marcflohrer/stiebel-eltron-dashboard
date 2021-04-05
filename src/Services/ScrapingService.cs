using HtmlAgilityPack;
using System.Threading.Tasks;
using System;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly IServiceWeltFacade _serviceWeltFacade;
        private readonly ITidyUpDirtyHtml _tidyUpDirtyHtml;
        private readonly IXpathService _xpathService;
        private string _sessionId = new Guid().ToString();

        public ScrapingService(IServiceWeltFacade serviceWeltFacade,
                               ITidyUpDirtyHtml tidyUpDirtyHtml,
                               IXpathService xpathService)
        {
            _serviceWeltFacade = serviceWeltFacade;
            _tidyUpDirtyHtml = tidyUpDirtyHtml;
            _xpathService = xpathService;
        }

        public async Task<HeatPump> GetHeatPumpInformationAsync(){
            return await GetHeatPumpInformationAsync(string.Empty, false);
        }

        public async Task<HeatPump> GetHeatPumpInformationAsync(string sessionId = "", bool retry = false)
        {
            if(!string.IsNullOrEmpty(sessionId) && string.IsNullOrEmpty(_sessionId)){
                _sessionId = sessionId;
            }else if(!string.IsNullOrEmpty(_sessionId) && string.IsNullOrEmpty(sessionId)){
                sessionId = _sessionId;
            }
            var htmlDocument = new HtmlDocument();
            var serviceWelt = await _serviceWeltFacade.GetHeatPumpWebsiteAsync(sessionId);
            _sessionId = serviceWelt.SessionId;
            var dirtyHtml = serviceWelt.HtmlDocument;
            var tidyHtml = _tidyUpDirtyHtml.GetTidyHtml(dirtyHtml);
            htmlDocument.LoadHtml(tidyHtml);
            var documentNode = htmlDocument.DocumentNode;
            var totalPowerConsumption = _xpathService.GetValueFor(htmlDocument, ScrapingValue.TotalPowerConsumption);

            if (totalPowerConsumption == null && !retry)
            {
                sessionId = await _serviceWeltFacade.LoginAsync();
                return await GetHeatPumpInformationAsync(sessionId, true);
            }
            else if (totalPowerConsumption == null && retry)
            {
                throw new Exception($"Unknown error while scraping ServiceWelt website.");
            }
            else
            {
                _sessionId = serviceWelt.SessionId;
                return new HeatPump
                {
                    TotalPowerConsumption = totalPowerConsumption
                };
            }
            throw new Exception($"Login failed for user!");
        }
    }
}