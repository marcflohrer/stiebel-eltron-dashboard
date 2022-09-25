using HtmlAgilityPack;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IWebsiteParser
    {
        public double GetValueFromWebsite(HtmlDocument htmlDocument, Metric scrapingValue);
        public string GetAttributeFromNode(HtmlDocument htmlDocument, Metric scrapingValue, string attributeName);
    }
}