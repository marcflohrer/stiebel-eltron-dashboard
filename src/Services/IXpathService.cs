using HtmlAgilityPack;

namespace StiebelEltronApiServer.Services
{
    public interface IXpathService
    {
        public string GetValueFor(HtmlDocument htmlDocument, ScrapingValue scrapingValue);
    }

    public enum ScrapingValue {
        TotalPowerConsumption
    }
}