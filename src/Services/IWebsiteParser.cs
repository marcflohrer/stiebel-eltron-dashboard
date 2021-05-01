using HtmlAgilityPack;

namespace StiebelEltronApiServer.Services
{
    public interface IWebsiteParser
    {
        public double GetValueFromSite(HtmlDocument htmlDocument, Metric scrapingValue);
    }
}