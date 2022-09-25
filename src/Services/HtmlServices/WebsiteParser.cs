using HtmlAgilityPack;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class WebsiteParser : IWebsiteParser
    {
        private readonly IUnitService _unitService;
        private readonly IValueParser _valueParser;
        private readonly IXpathService _xpathService;
        public WebsiteParser(IUnitService unitService, IValueParser valueParser, IXpathService xpathService)
        {
            _xpathService = xpathService;
            _valueParser = valueParser;
            _unitService = unitService;
        }
        public double GetValueFromWebsite(HtmlDocument htmlDocument, Metric scrapingValue)
            => _unitService.GetBaseUnitValue(_valueParser.GetValueWithUnit(_xpathService.GetValueFor(htmlDocument, scrapingValue)));

        public string GetAttributeFromNode(HtmlDocument htmlDocument, Metric scrapingValue, string attributeName)
            => _xpathService.GetAttributeValue(htmlDocument, scrapingValue, attributeName);
    }
}