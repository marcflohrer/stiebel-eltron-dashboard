using AutoFixture;
using AutoMoqCore;
using Moq;
using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Services;
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
                SessionId = sessionId,
                HtmlDocument = ServiceWeltMockData.HeatPumpWebsite
            }));
            var tidyUpDirtyHtml = autoMoqer.GetMock<ITidyUpDirtyHtml>();
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(It.IsAny<string>())).Returns(ServiceWeltMockData.HeatPumpWebsiteTidiedUp);
            var xpathService = autoMoqer.Create<XpathService>();
            autoMoqer.SetInstance<IXpathService>(xpathService);
            var scrapingService = autoMoqer.Create<ScrapingService>();

            // Act
            var result = scrapingService.GetHeatPumpInformationAsync(sessionId, false).Result;

            // Assert
            Assert.Equal("17,739MWh", result.TotalPowerConsumption);
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
                SessionId = sessionNotLoggedIn,
                HtmlDocument = ServiceWeltMockData.LoginWebSite
            }));
            var sessionLoggedIn = "LOGGEDIN";
            _ = serviceWeltFacade.Setup(mock => mock.GetHeatPumpWebsiteAsync(It.Is<string>(i => i == sessionLoggedIn))).Returns(Task.FromResult(new ServiceWelt()
            {
                SessionId = sessionLoggedIn,
                HtmlDocument = ServiceWeltMockData.HeatPumpWebsite
            }));
            serviceWeltFacade.Setup(mock => mock.LoginAsync()).Returns(Task.FromResult(sessionLoggedIn));
            var tidyUpDirtyHtml = autoMoqer.GetMock<ITidyUpDirtyHtml>();
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(ServiceWeltMockData.LoginWebSite)).Returns(ServiceWeltMockData.LoginWebSite);
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(ServiceWeltMockData.HeatPumpWebsite)).Returns(ServiceWeltMockData.HeatPumpWebsiteTidiedUp);
            var xpathService = autoMoqer.Create<XpathService>();
            autoMoqer.SetInstance<IXpathService>(xpathService);
            var scrapingService = autoMoqer.Create<ScrapingService>();

            // Act
            var result = scrapingService.GetHeatPumpInformationAsync(sessionNotLoggedIn, false).Result;

            // Assert
            Assert.Equal("17,739MWh", result.TotalPowerConsumption);
            serviceWeltFacade.Verify(mock => mock.LoginAsync(), Times.Once);
        }
    }
}
