using AutoMoqCore;
using StiebelEltronApiServer.Services.HtmlServices;
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
        public void WhenTidyingUpCleanHtmlSameIsReturned(int testDataIndex)
        {
            var autoMoqer = new AutoMoqer();
            var htmlParser = autoMoqer.Create<HtmlScanner>();
            autoMoqer.SetInstance<IHtmlScanner>(htmlParser);
            var tidyUpDirtyHtml = autoMoqer.Create<TidyUpDirtyHtml>();
            var inputHtml = ServiceWeltMockData.GetHtml(testDataIndex);
            var expectedHtml = inputHtml.Replace("&nbsp;", string.Empty);
            expectedHtml = expectedHtml.Replace("&copy;", string.Empty);
            
            _output.WriteLine("Testing TestSnippet" +testDataIndex +".html" );
            var tidyHtml = tidyUpDirtyHtml.GetTidyHtml(inputHtml);

            Assert.Equal(expectedHtml, tidyHtml);
        }

        [Theory]
        [InlineData("<a><b></a>", "<a><b></b></a>")]
        [InlineData("<a><b><p></p></a>", "<a><b><p></p></b></a>")]
        [InlineData("<a><p></p></b></a>", "<a><p></p>    </a>")]
        public void WhenTidyingUpCleanHtmlIsReturned(string dirtyHtml, string exptectedTidyHtml)
        {
            var autoMoqer = new AutoMoqer();
            var htmlParser = autoMoqer.Create<HtmlScanner>();
            autoMoqer.SetInstance<IHtmlScanner>(htmlParser);
            var tidyUpDirtyHtml = autoMoqer.Create<TidyUpDirtyHtml>();
            
            _output.WriteLine($"Cleaning {dirtyHtml}" );
            var actualTidyHtml = tidyUpDirtyHtml.GetTidyHtml(dirtyHtml);
            _output.WriteLine($"Cleaned: {actualTidyHtml}" );

            Assert.Equal(exptectedTidyHtml, actualTidyHtml);
        }        

        [Fact]
        public void WhenTidyingUpDirtyHtmlTidyHtmlIsReturned()
        {
            var autoMoqer = new AutoMoqer();
            var htmlParser = autoMoqer.Create<HtmlScanner>();
            autoMoqer.SetInstance<IHtmlScanner>(htmlParser);
            var tidyUpDirtyHtml = autoMoqer.Create<TidyUpDirtyHtml>();
            var inputHtml = ServiceWeltMockData.HeatPumpWebsite;
            var outputHtml = ServiceWeltMockData.HeatPumpWebsiteTidiedUp;
            
            var tidyHtml = tidyUpDirtyHtml.GetTidyHtml(inputHtml);
            Assert.Equal(outputHtml, tidyHtml);
        }
    }
}
