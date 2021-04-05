using HtmlAgilityPack;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Services
{
    public class XpathService : IXpathService
    {        
        private readonly IDictionary<ScrapingValue, string> xpathOfElement = new Dictionary<ScrapingValue, string>()
        {            
            { ScrapingValue.TotalPowerConsumption, "/html[1]/body[1]/div[2]/div[1]/form[1]/div[1]/div[2]/table[1]/tr[3]/td[2]" },
        };

        public string GetValueFor(HtmlDocument htmlDocument, ScrapingValue scrapingValue) {
            return htmlDocument.DocumentNode
                .SelectSingleNode(xpathOfElement[scrapingValue])?
                .InnerText;
        }
    }
}