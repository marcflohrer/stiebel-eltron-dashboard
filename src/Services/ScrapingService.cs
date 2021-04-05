using HtmlAgilityPack;
using System.Threading.Tasks;
using System;
using StiebelEltronApiServer.Models;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly IServiceWeltFacade _serviceWeltFacade;
        private readonly ITidyUpDirtyHtml _tidyUpDirtyHtml;
        private string _sessionId = new Guid().ToString();
        private readonly IDictionary<string, string> xpathOfElement = new Dictionary<string, string>()
        {            
            { "totalPowerConsumption", "/html[1]/body[1]/div[2]/div[1]/form[1]/div[1]/div[2]/table[1]/tr[3]/td[2]" },
        };

        public ScrapingService(IServiceWeltFacade serviceWeltFacade, ITidyUpDirtyHtml tidyUpDirtyHtml)
        {
            _serviceWeltFacade = serviceWeltFacade;
            _tidyUpDirtyHtml = tidyUpDirtyHtml;
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
            var rawTotalPowerConsumption = documentNode
                .SelectSingleNode(xpathOfElement["totalPowerConsumption"])?
                .InnerText;
            
            if (rawTotalPowerConsumption == null && !retry)
            {
                sessionId = await _serviceWeltFacade.LoginAsync();
                return await GetHeatPumpInformationAsync(sessionId, true);
            }
            else if (rawTotalPowerConsumption == null && retry)
            {
                throw new Exception($"Unknown error while scraping ServiceWelt website.");
            }
            else
            {
                _sessionId = serviceWelt.SessionId;
                return new HeatPump
                {
                    TotalPowerConsumption = rawTotalPowerConsumption
                };
            }
            throw new Exception($"Login failed for user!");
        }
    }
}