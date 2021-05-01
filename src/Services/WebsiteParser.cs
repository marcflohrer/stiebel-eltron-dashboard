using HtmlAgilityPack;

namespace StiebelEltronApiServer.Services {
    public class WebsiteParser : IWebsiteParser {
        private readonly IUnitService _unitService;
        private readonly IValueParser _valueParser;
        private readonly IXpathService _xpathService;
        public WebsiteParser (IUnitService unitService, IValueParser valueParser, IXpathService xpathService) {
            _xpathService = xpathService;
            _valueParser = valueParser;
            _unitService = unitService;
        }
        public double GetValueFromSite (HtmlDocument htmlDocument, Metric scrapingValue) {
            return _unitService.GetBaseUnitValue(_valueParser.GetValueWithUnit(_xpathService.GetValueFor (htmlDocument, Metric.TotalPowerConsumption)));
        }
    }
}