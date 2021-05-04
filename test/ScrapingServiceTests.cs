using AutoFixture;
using AutoMoqCore;
using Moq;
using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Services;
using StiebelEltronApiServer.Services.HtmlServices;
using System.Threading.Tasks;
using Xunit;

namespace StiebelEltronApiServerTests
{
    public class ScrapingServiceTests
    {
        [Fact]
        public void WhenScrapingServiceWeltTotalPowerConsumptionIsReturned()
        {
            var autoMoqer = new AutoMoqer();
            var fixture = new Fixture();
            var sessionId = fixture.Create<string>();
            var serviceWeltFacade = autoMoqer.GetMock<IServiceWeltFacade>();
            _ = serviceWeltFacade.Setup(mock => mock.GetHeatPumpWebsiteAsync(It.IsAny<string>())).Returns(Task.FromResult(new ServiceWelt()
            {
                HtmlDocument = ServiceWeltMockData.HeatPumpWebsite
            }));
            var tidyUpDirtyHtml = autoMoqer.GetMock<ITidyUpDirtyHtml>();
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(It.IsAny<string>())).Returns(ServiceWeltMockData.HeatPumpWebsiteTidiedUp);
            var xpathService = autoMoqer.Create<XpathService>();
            autoMoqer.SetInstance<IXpathService>(xpathService);
            
            var unitService = autoMoqer.Create<UnitService>();
            autoMoqer.SetInstance<IUnitService>(unitService);
            var valueParser = autoMoqer.Create<ValueParser>();
            autoMoqer.SetInstance<IValueParser>(valueParser);
            var websiteParser = autoMoqer.Create<WebsiteParser>();
            autoMoqer.SetInstance<IWebsiteParser>(websiteParser);

            var scrapingService = autoMoqer.Create<ScrapingService>();

            // Act
            var result = scrapingService.GetHeatPumpInformationAsync(sessionId).Result;

            // Assert
            Assert.Equal(17739000.0, result.TotalPowerConsumption);
        }

        [Fact]
        public void WhenNotLoggedScrapingServiceWeltTotalPowerConsumptionIsReturned()
        {
            var autoMoqer = new AutoMoqer();
            var fixture = new Fixture();
            var serviceWeltFacade = autoMoqer.GetMock<IServiceWeltFacade>();
            var sessionNotLoggedIn = "NOTLOGGEDIN";
            _ = serviceWeltFacade.Setup(mock => mock.GetHeatPumpWebsiteAsync(It.Is<string>(i => i == sessionNotLoggedIn))).Returns(Task.FromResult(new ServiceWelt()
            {
                HtmlDocument = ServiceWeltMockData.HeatPumpWebsite
            }));
            var tidyUpDirtyHtml = autoMoqer.GetMock<ITidyUpDirtyHtml>();
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(ServiceWeltMockData.LoginWebSite)).Returns(ServiceWeltMockData.LoginWebSite);
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(ServiceWeltMockData.HeatPumpWebsite)).Returns(ServiceWeltMockData.HeatPumpWebsiteTidiedUp);
            var xpathService = autoMoqer.Create<XpathService>();
            autoMoqer.SetInstance<IXpathService>(xpathService);

            var unitService = autoMoqer.Create<UnitService>();
            autoMoqer.SetInstance<IUnitService>(unitService);
            var valueParser = autoMoqer.Create<ValueParser>();
            autoMoqer.SetInstance<IValueParser>(valueParser);
            var websiteParser = autoMoqer.Create<WebsiteParser>();
            autoMoqer.SetInstance<IWebsiteParser>(websiteParser);

            var scrapingService = autoMoqer.Create<ScrapingService>();

            // Act
            var result = scrapingService.GetHeatPumpInformationAsync(sessionNotLoggedIn).Result;

            // Assert
            Assert.Equal(17739000.0, result.TotalPowerConsumption);
        }
    }
}
