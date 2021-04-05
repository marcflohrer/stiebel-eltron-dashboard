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
            { "totalPowerConsumption", "//*[@id=id\"content\"]/div[2]/table/tbody/tr[3]/td[2]" },
        };

        public ScrapingService(IServiceWeltFacade serviceWeltFacade, ITidyUpDirtyHtml tidyUpDirtyHtml)
        {
            _serviceWeltFacade = serviceWeltFacade;
            _tidyUpDirtyHtml = tidyUpDirtyHtml;
        }

        public async Task<HeatPump> GetHeatPumpInformationAsync(bool retry)
        {
            var htmlDocument = new HtmlDocument();
            var content = await _serviceWeltFacade.GetHeatPumpWebsiteAsync(_sessionId);
            _sessionId = content.SessionId;
            var dirtyHtml = content.HtmlDocument;
            var tidyHtml = _tidyUpDirtyHtml.GetTidyHtml(dirtyHtml);
            htmlDocument.LoadHtml(tidyHtml);
            var totalPowerConsumptionUnparsed = htmlDocument.DocumentNode
             .SelectSingleNode("//*[@id=\"content\"]/div[2]/table/tbody/tr[3]/td[2]")
             .Attributes["value"].Value;
            if (totalPowerConsumptionUnparsed == null && !retry)
            {
                _sessionId = await _serviceWeltFacade.LoginAsync();
                return await GetHeatPumpInformationAsync(true);
            }
            else if (totalPowerConsumptionUnparsed == null && retry)
            {
                throw new Exception($"Unknown error while scraping ServiceWelt website.");
            }
            else
            {
                _sessionId = content.SessionId;
                return new HeatPump
                {
                    TotalPowerConsumption = totalPowerConsumptionUnparsed
                };
            }
            throw new Exception($"Login failed for user!");
        }
    }
}