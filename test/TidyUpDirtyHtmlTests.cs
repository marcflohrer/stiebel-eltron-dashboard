using AutoMoqCore;
using StiebelEltronApiServer.Services;
using Xunit;
using Xunit.Abstractions;

namespace StiebelEltronApiServerTests
{
    public class TidyUpDirtyHtmlTests
    {
        private readonly ITestOutputHelper _output;

        public TidyUpDirtyHtmlTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        public void WhenScrapingServiceWeltTotalPowerConsumptionIsReturned(int testDataIndex)
        {
            var autoMoqer = new AutoMoqer();
            var tidyUpDirtyHtml = autoMoqer.Create<TidyUpDirtyHtml>();
            var inputHtml = ServiceWeltMockData.GetHtml(testDataIndex);
            var outputHtml = inputHtml.Replace("&nbsp;", string.Empty);
            outputHtml = outputHtml.Replace("&copy;", string.Empty);
            
            _output.WriteLine("Testing TestSnippet" +testDataIndex +".html" );
            var tidyHtml = tidyUpDirtyHtml.GetTidyHtml(inputHtml);

            Assert.Equal(outputHtml, tidyHtml);
        }
    }
}
