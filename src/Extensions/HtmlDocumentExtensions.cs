using HtmlAgilityPack;
using StiebelEltronDashboard.Services.HtmlServices;

namespace StiebelEltronDashboard.Extensions
{
    public static class HtmlDocumentExtensions
    {
        private static IWebsiteParser _websiteParser;

        public static IWebsiteParser WebsiteParser
        {
            get
            {
                return _websiteParser;
            }
            set
            {
                _websiteParser = value;
            }
        }

        public static double ParseFor(this HtmlDocument htmlDocument,
            Metric scrapingValue) =>
            _websiteParser.GetValueFromWebsite(
                htmlDocument,
                scrapingValue);

        public static string ParseForAttribute(this HtmlDocument htmlDocument,
            Metric scrapingValue,
            string attributeName)
            => _websiteParser.GetAttributeFromNode(
                htmlDocument,
                scrapingValue,
                attributeName);
    }
}