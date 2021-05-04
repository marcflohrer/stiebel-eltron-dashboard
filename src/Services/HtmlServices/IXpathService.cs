using HtmlAgilityPack;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public interface IXpathService
    {
        public string GetValueFor(HtmlDocument htmlDocument, Metric scrapingValue);
    }

    public enum Metric
    {
        TotalPowerConsumption,
        ActualSpeedDensifier,
        AntiFreezeTemperature,
        CompressorInletTemperature,
        CondenserTemperature,
        DateCreated,
        DateUpdated,
        DefrostStarts,
        DefrostTime,
        EvaporatorTemperature,
        ExhaustAirTemperature,
        FanPowerRel,
        HighPressure,
        HotGasTemperature,
        InletTemperature,
        IntermediateInjectionTemperature,
        LowPressure,
        OilSumpTemperature,
        OutdoorTemperature,
        PowerConsumptionHeatingDay,
        PowerConsumptionHeatingSum,
        PowerConsumptionHotWaterDay,
        PowerConsumptionHotWaterSum,
        PressureMedium,
        ReheatingStages1,
        ReheatingStages2,
        ReheatingStagesHeatQuantityHeatingSum,
        ReheatingStagesHeatQuantityHotWaterTotal,
        ReturnTemperature,
        RuntimeVaporizerDefrost,
        RuntimeVaporizerHeating,
        RuntimeVaporizerHotWater,
        SettingSpeedCompressed,
        VaporizerHeatQuantityHeatingDay,
        VaporizerHeatQuantityHeatingTotal,
        VaporizerHeatQuantityHotWaterDay,
        VaporizerHeatQuantityHotWaterTotal,
        VoltageInverter,
        WaterVolumeCurrent,

    }
}