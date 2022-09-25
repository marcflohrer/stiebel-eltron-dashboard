using AutoMoqCore;
using HtmlAgilityPack;
using StiebelEltronDashboard.Services;
using StiebelEltronDashboard.Services.HtmlServices;
using System;
using Xunit;
using Xunit.Abstractions;

namespace StiebelEltronDashboardTests
{
    public class WebsiteServiceTest
    {
        private readonly ITestOutputHelper testOutputHelper;
        public WebsiteServiceTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        public enum DecimalSeparator { Comma, Dot }

        [Theory]
        [InlineData(Metric.ReturnTemperature, 25.3, DecimalSeparator.Comma)]
        [InlineData(Metric.InletTemperature, 34.1, DecimalSeparator.Comma)]
        [InlineData(Metric.AntiFreezeTemperature, 32.9, DecimalSeparator.Comma)]
        [InlineData(Metric.OutdoorTemperature, 7.7, DecimalSeparator.Comma)]
        [InlineData(Metric.ExhaustAirTemperature, 1.0, DecimalSeparator.Comma)]
        [InlineData(Metric.EvaporatorTemperature, 0.7, DecimalSeparator.Comma)]
        [InlineData(Metric.CompressorInletTemperature, 0.4, DecimalSeparator.Comma)]
        [InlineData(Metric.IntermediateInjectionTemperature, 28.2, DecimalSeparator.Comma)]
        [InlineData(Metric.HotGasTemperature, 47.8, DecimalSeparator.Comma)]
        [InlineData(Metric.CondenserTemperature, 30, DecimalSeparator.Comma)]
        [InlineData(Metric.OilSumpTemperature, 12.6, DecimalSeparator.Comma)]
        [InlineData(Metric.LowPressure, 7.78, DecimalSeparator.Comma)]
        [InlineData(Metric.PressureMedium, 10.87, DecimalSeparator.Comma)]
        [InlineData(Metric.HighPressure, 20.5, DecimalSeparator.Comma)]
        [InlineData(Metric.WaterVolumeCurrent, 15.3, DecimalSeparator.Comma)]
        [InlineData(Metric.VoltageInverter, 403, DecimalSeparator.Comma)]
        [InlineData(Metric.ActualSpeedDensifier, 50, DecimalSeparator.Comma)]
        [InlineData(Metric.SettingSpeedCompressed, 50, DecimalSeparator.Comma)]
        [InlineData(Metric.FanPowerRel, 42, DecimalSeparator.Comma)]
        [InlineData(Metric.VaporizerHeatQuantityHeatingDay, 53221, DecimalSeparator.Comma)]
        [InlineData(Metric.VaporizerHeatQuantityHeatingTotal, 17739000, DecimalSeparator.Comma)]
        [InlineData(Metric.VaporizerHeatQuantityHotWaterDay, 13234, DecimalSeparator.Comma)]
        [InlineData(Metric.VaporizerHeatQuantityHotWaterTotal, 2282000, DecimalSeparator.Comma)]
        [InlineData(Metric.ReheatingStagesHeatQuantityHeatingSum, 67000, DecimalSeparator.Comma)]
        [InlineData(Metric.ReheatingStagesHeatQuantityHotWaterTotal, 20000, DecimalSeparator.Comma)]
        [InlineData(Metric.PowerConsumptionHeatingDay, 10748, DecimalSeparator.Comma)]
        [InlineData(Metric.PowerConsumptionHeatingSum, 4421000, DecimalSeparator.Comma)]
        [InlineData(Metric.PowerConsumptionHotWaterDay, 4046.9999999999995, DecimalSeparator.Comma)]
        [InlineData(Metric.PowerConsumptionHotWaterSum, 770000, DecimalSeparator.Comma)]
        [InlineData(Metric.RuntimeVaporizerHeating, 1960, DecimalSeparator.Comma)]
        [InlineData(Metric.RuntimeVaporizerHotWater, 256, DecimalSeparator.Comma)]
        [InlineData(Metric.RuntimeVaporizerDefrost, 55, DecimalSeparator.Comma)]
        [InlineData(Metric.ReheatingStages1, 0, DecimalSeparator.Comma)]
        [InlineData(Metric.ReheatingStages2, 0, DecimalSeparator.Comma)]
        [InlineData(Metric.DefrostTime, 2, DecimalSeparator.Comma)]
        [InlineData(Metric.DefrostStarts, 742, DecimalSeparator.Comma)]
        [InlineData(Metric.ReturnTemperature, 25.3, DecimalSeparator.Dot)]
        [InlineData(Metric.InletTemperature, 34.1, DecimalSeparator.Dot)]
        [InlineData(Metric.AntiFreezeTemperature, 32.9, DecimalSeparator.Dot)]
        [InlineData(Metric.OutdoorTemperature, 7.7, DecimalSeparator.Dot)]
        [InlineData(Metric.ExhaustAirTemperature, 1.0, DecimalSeparator.Dot)]
        [InlineData(Metric.EvaporatorTemperature, 0.7, DecimalSeparator.Dot)]
        [InlineData(Metric.CompressorInletTemperature, 0.4, DecimalSeparator.Dot)]
        [InlineData(Metric.IntermediateInjectionTemperature, 28.2, DecimalSeparator.Dot)]
        [InlineData(Metric.HotGasTemperature, 47.8, DecimalSeparator.Dot)]
        [InlineData(Metric.CondenserTemperature, 30, DecimalSeparator.Dot)]
        [InlineData(Metric.OilSumpTemperature, 12.6, DecimalSeparator.Dot)]
        [InlineData(Metric.LowPressure, 7.78, DecimalSeparator.Dot)]
        [InlineData(Metric.PressureMedium, 10.87, DecimalSeparator.Dot)]
        [InlineData(Metric.HighPressure, 20.5, DecimalSeparator.Dot)]
        [InlineData(Metric.WaterVolumeCurrent, 15.3, DecimalSeparator.Dot)]
        [InlineData(Metric.VoltageInverter, 403, DecimalSeparator.Dot)]
        [InlineData(Metric.ActualSpeedDensifier, 50, DecimalSeparator.Dot)]
        [InlineData(Metric.SettingSpeedCompressed, 50, DecimalSeparator.Dot)]
        [InlineData(Metric.FanPowerRel, 42, DecimalSeparator.Dot)]
        [InlineData(Metric.VaporizerHeatQuantityHeatingDay, 53221, DecimalSeparator.Dot)]
        [InlineData(Metric.VaporizerHeatQuantityHeatingTotal, 17739000, DecimalSeparator.Dot)]
        [InlineData(Metric.VaporizerHeatQuantityHotWaterDay, 13234, DecimalSeparator.Dot)]
        [InlineData(Metric.VaporizerHeatQuantityHotWaterTotal, 2282000, DecimalSeparator.Dot)]
        [InlineData(Metric.ReheatingStagesHeatQuantityHeatingSum, 67000, DecimalSeparator.Dot)]
        [InlineData(Metric.ReheatingStagesHeatQuantityHotWaterTotal, 20000, DecimalSeparator.Dot)]
        [InlineData(Metric.PowerConsumptionHeatingDay, 10748, DecimalSeparator.Dot)]
        [InlineData(Metric.PowerConsumptionHeatingSum, 4421000, DecimalSeparator.Dot)]
        [InlineData(Metric.PowerConsumptionHotWaterDay, 4046.9999999999995, DecimalSeparator.Dot)]
        [InlineData(Metric.PowerConsumptionHotWaterSum, 770000, DecimalSeparator.Dot)]
        [InlineData(Metric.RuntimeVaporizerHeating, 1960, DecimalSeparator.Dot)]
        [InlineData(Metric.RuntimeVaporizerHotWater, 256, DecimalSeparator.Dot)]
        [InlineData(Metric.RuntimeVaporizerDefrost, 55, DecimalSeparator.Dot)]
        [InlineData(Metric.ReheatingStages1, 0, DecimalSeparator.Dot)]
        [InlineData(Metric.ReheatingStages2, 0, DecimalSeparator.Dot)]
        [InlineData(Metric.DefrostTime, 2, DecimalSeparator.Dot)]
        [InlineData(Metric.DefrostStarts, 742, DecimalSeparator.Dot)]
        public void WhenParsingHtmlDocumentForValuesCorrectDoublesAreReturned(Metric metric, double expectedValue, DecimalSeparator decimalSeparator)
        {
            var autoMoqer = new AutoMoqer();

            var xpathService = autoMoqer.Create<XpathService>();
            autoMoqer.SetInstance<IXpathService>(xpathService);
            var valueParser = autoMoqer.Create<ValueParser>();
            autoMoqer.SetInstance<IValueParser>(valueParser);
            var unitService = autoMoqer.Create<UnitService>();
            autoMoqer.SetInstance<IUnitService>(unitService);

            var websiteParser = autoMoqer.Create<WebsiteParser>();
            autoMoqer.SetInstance<IWebsiteParser>(websiteParser);

            var scrapingService = autoMoqer.Create<WebsiteParser>();

            var htmlDocument = new HtmlDocument();
            if (decimalSeparator == DecimalSeparator.Comma)
            {
                htmlDocument.LoadHtml(ServiceWeltMockData.HeatPumpWebsite);
            }
            else
            {
                htmlDocument.LoadHtml(ServiceWeltMockData.HeatPumpWebsiteDot);
            }


            this.testOutputHelper.WriteLine($" >>> {metric} --> {expectedValue}");

            // Act
            var actualValue = scrapingService.GetValueFromWebsite(htmlDocument, metric);

            // Assert
            Assert.Equal(expectedValue, actualValue, double.Epsilon);
        }
    }
}