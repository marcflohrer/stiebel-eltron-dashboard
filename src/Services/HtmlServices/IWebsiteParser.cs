using HtmlAgilityPack;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IWebsiteParser
    {
        public double GetValueFromSite(HtmlDocument htmlDocument, Metric scrapingValue);
    }
}