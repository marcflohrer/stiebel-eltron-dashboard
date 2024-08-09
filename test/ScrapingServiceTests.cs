using AutoFixture;
using AutoMoqCore;
using Moq;
using StiebelEltronDashboard.Models;
using StiebelEltronDashboard.Services;
using StiebelEltronDashboard.Services.HtmlServices;
using System.Threading.Tasks;
using Xunit;

namespace StiebelEltronDashboardTests
{
    public class ScrapingServiceTests
    {
        [Fact]
        public async Task WhenScrapingServiceWeltTotalPowerConsumptionIsReturned()
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

            var scrapingService = autoMoqer.Create<ServiceWeltService>();

            // Act
            var result = await scrapingService.GetHeatPumpInformationAsync(sessionId);

            // Assert
            Assert.Equal(17739000.0, result.TotalPowerConsumption);
        }

        [Fact]
        public async Task WhenNotLoggedScrapingServiceWeltTotalPowerConsumptionIsReturned()
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

            var scrapingService = autoMoqer.Create<ServiceWeltService>();

            // Act
            var result = await scrapingService.GetHeatPumpInformationAsync(sessionNotLoggedIn);

            // Assert
            Assert.Equal(17739000.0, result.TotalPowerConsumption);
        }
    }
}
