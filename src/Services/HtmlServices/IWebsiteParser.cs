using HtmlAgilityPack;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public interface IWebsiteParser
    {
        public double GetValueFromSite(HtmlDocument htmlDocument, Metric scrapingValue);
    }
}