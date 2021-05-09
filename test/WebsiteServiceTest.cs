using AutoFixture;
using AutoMoqCore;
using HtmlAgilityPack;
using StiebelEltronApiServer.Services;
using StiebelEltronApiServer.Services.HtmlServices;
using Xunit;
using Xunit.Abstractions;

namespace StiebelEltronApiServerTests {
    public class WebsiteServiceTest {
        private readonly ITestOutputHelper testOutputHelper;
        public WebsiteServiceTest (ITestOutputHelper testOutputHelper) {
            this.testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData (Metric.ReturnTemperature, 25.3)]
        [InlineData (Metric.InletTemperature, 34.1)]
        [InlineData (Metric.AntiFreezeTemperature, 32.9)]
        [InlineData (Metric.OutdoorTemperature, 7.7)]
        [InlineData (Metric.ExhaustAirTemperature, 1.0)]
        [InlineData (Metric.EvaporatorTemperature, 0.7)]
        [InlineData (Metric.CompressorInletTemperature, 0.4)]
        [InlineData (Metric.IntermediateInjectionTemperature, 28.2)]
        [InlineData (Metric.HotGasTemperature, 47.8)]
        [InlineData (Metric.CondenserTemperature, 30)]
        [InlineData (Metric.OilSumpTemperature, 12.6)]
        [InlineData (Metric.LowPressure, 7.78)]
        [InlineData (Metric.PressureMedium, 10.87)]
        [InlineData (Metric.HighPressure, 20.5)]
        [InlineData (Metric.WaterVolumeCurrent, 15.3)]
        [InlineData (Metric.VoltageInverter, 403)]
        [InlineData (Metric.ActualSpeedDensifier, 50)]
        [InlineData (Metric.SettingSpeedCompressed, 50)]
        [InlineData (Metric.FanPowerRel, 42)]
        [InlineData (Metric.VaporizerHeatQuantityHeatingDay, 53221)]
        [InlineData (Metric.VaporizerHeatQuantityHeatingTotal, 17739000)]
        [InlineData (Metric.VaporizerHeatQuantityHotWaterDay, 13234)]
        [InlineData (Metric.VaporizerHeatQuantityHotWaterTotal, 2282000)]
        [InlineData (Metric.ReheatingStagesHeatQuantityHeatingSum, 67000)]
        [InlineData (Metric.ReheatingStagesHeatQuantityHotWaterTotal, 20000)]
        [InlineData (Metric.PowerConsumptionHeatingDay, 10748)]
        [InlineData (Metric.PowerConsumptionHeatingSum, 4421000)]
        [InlineData (Metric.PowerConsumptionHotWaterDay, 4046.9999999999995)]
        [InlineData (Metric.PowerConsumptionHotWaterSum, 770000)]
        [InlineData (Metric.RuntimeVaporizerHeating, 1960)]
        [InlineData (Metric.RuntimeVaporizerHotWater, 256)]
        [InlineData (Metric.RuntimeVaporizerDefrost, 55)]
        [InlineData (Metric.ReheatingStages1, 0)]
        [InlineData (Metric.ReheatingStages2, 0)]
        [InlineData (Metric.DefrostTime, 2)]
        [InlineData (Metric.DefrostStarts, 742)]
        public void WhenParsingHtmlDocumentForValuesCorrectDoublesAreReturned (Metric metric, double expectedValue) {
            var autoMoqer = new AutoMoqer ();

            var fixture = new Fixture ();

            var xpathService = autoMoqer.Create<XpathService> ();
            autoMoqer.SetInstance<IXpathService> (xpathService);
            var valueParser = autoMoqer.Create<ValueParser> ();
            autoMoqer.SetInstance<IValueParser> (valueParser);
            var unitService = autoMoqer.Create<UnitService> ();
            autoMoqer.SetInstance<IUnitService> (unitService);

            var websiteParser = autoMoqer.Create<WebsiteParser> ();
            autoMoqer.SetInstance<IWebsiteParser> (websiteParser);

            var scrapingService = autoMoqer.Create<WebsiteParser> ();

            var htmlDocument = new HtmlDocument ();
            htmlDocument.LoadHtml (ServiceWeltMockData.HeatPumpWebsite);
            
            this.testOutputHelper.WriteLine($" >>> {metric} --> {expectedValue}" );
            
            // Act
            var actualValue = scrapingService.GetValueFromSite (htmlDocument, metric);

            // Assert
            Assert.Equal (expectedValue, actualValue);
        }
    }
}