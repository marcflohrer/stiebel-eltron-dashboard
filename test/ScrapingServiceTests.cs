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
            var serviceWeltFacade = autoMoqer.GetMock<IServiceWeltFacade>();
            _ = serviceWeltFacade.Setup(mock => mock.GetHeatPumpWebsiteAsync(It.IsAny<string>())).Returns(Task.FromResult(new ServiceWelt()
            {
                SessionId = fixture.Create<string>(),
                HtmlDocument = ServiceWeltMockData.HeatPumpWebsite
            }));
            var tidyUpDirtyHtml = autoMoqer.GetMock<ITidyUpDirtyHtml>();
            _ = tidyUpDirtyHtml.Setup(mock => mock.GetTidyHtml(It.IsAny<string>())).Returns(ServiceWeltMockData.HeatPumpWebsiteTidiedUp);
            var scrapingService = new ScrapingService(serviceWeltFacade.Object, tidyUpDirtyHtml.Object);

            // Act
            var result = scrapingService.GetHeatPumpInformationAsync(false).Result;

            // Assert
            Assert.Equal("17,739MWh", result.TotalPowerConsumption);
        }
    }
}
