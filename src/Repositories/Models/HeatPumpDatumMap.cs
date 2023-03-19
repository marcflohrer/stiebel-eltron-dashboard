using CsvHelper.Configuration;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Repositories.Models;
public sealed class HeatPumpDatumMap : ClassMap<HeatPumpDatum>
{
    public HeatPumpDatumMap()
    {
        Map(m => m.TotalPowerConsumption).Index(1).Name("TotalPowerConsumption");
        Map(m => m.ActualSpeedDensifier).Index(2).Name("ActualSpeedDensifier");
        Map(m => m.AntiFreezeTemperature).Index(3).Name("AntiFreezeTemperature");
        Map(m => m.CompressorInletTemperature).Index(4).Name("CompressorInletTemperature");
        Map(m => m.CondenserTemperature).Index(5).Name("CondenserTemperature");
        Map(m => m.DateCreated).Index(6).Name("DateCreated");
        Map(m => m.DateUpdated).Index(7).Name("DateUpdated");
        Map(m => m.DefrostStarts).Index(8).Name("DefrostStarts");
        Map(m => m.DefrostTime).Index(9).Name("DefrostTime");
        Map(m => m.EvaporatorTemperature).Index(10).Name("EvaporatorTemperature");
        Map(m => m.ExhaustAirTemperature).Index(11).Name("ExhaustAirTemperature");
        Map(m => m.FanPowerRel).Index(12).Name("FanPowerRel");
        Map(m => m.HighPressure).Index(13).Name("HighPressure");
        Map(m => m.HotGasTemperature).Index(14).Name("HotGasTemperature");
        Map(m => m.InletTemperature).Index(15).Name("InletTemperature");
        Map(m => m.IntermediateInjectionTemperature).Index(16).Name("IntermediateInjectionTemperature");
        Map(m => m.LowPressure).Index(17).Name("LowPressure");
        Map(m => m.OilSumpTemperature).Index(18).Name("OilSumpTemperature");
        Map(m => m.OutdoorTemperature).Index(19).Name("OutdoorTemperature");
        Map(m => m.PowerConsumptionHeatingDay).Index(20).Name("PowerConsumptionHeatingDay");
        Map(m => m.PowerConsumptionHeatingSum).Index(21).Name("PowerConsumptionHeatingSum");
        Map(m => m.PowerConsumptionHotWaterDay).Index(22).Name("PowerConsumptionHotWaterDay");
        Map(m => m.PowerConsumptionHotWaterSum).Index(23).Name("PowerConsumptionHotWaterSum");
        Map(m => m.PressureMedium).Index(24).Name("PressureMedium");
        Map(m => m.ReheatingStages1).Index(25).Name("ReheatingStages1");
        Map(m => m.ReheatingStages2).Index(26).Name("ReheatingStages2");
        Map(m => m.ReheatingStagesHeatQuantityHeatingSum).Index(27).Name("ReheatingStagesHeatQuantityHeatingSum");
        Map(m => m.ReheatingStagesHeatQuantityHotWaterTotal).Index(28).Name("ReheatingStagesHeatQuantityHotWaterTotal");
        Map(m => m.ReturnTemperature).Index(29).Name("ReturnTemperature");
        Map(m => m.RuntimeVaporizerDefrost).Index(30).Name("RuntimeVaporizerDefrost");
        Map(m => m.RuntimeVaporizerHeating).Index(31).Name("RuntimeVaporizerHeating");
        Map(m => m.RuntimeVaporizerHotWater).Index(32).Name("RuntimeVaporizerHotWater");
        Map(m => m.SettingSpeedCompressed).Index(33).Name("SettingSpeedCompressed");
        Map(m => m.VaporizerHeatQuantityHeatingDay).Index(34).Name("VaporizerHeatQuantityHeatingDay");
        Map(m => m.VaporizerHeatQuantityHeatingTotal).Index(35).Name("VaporizerHeatQuantityHeatingTotal");
        Map(m => m.VaporizerHeatQuantityHotWaterDay).Index(36).Name("VaporizerHeatQuantityHotWaterDay");
        Map(m => m.VaporizerHeatQuantityHotWaterTotal).Index(37).Name("VaporizerHeatQuantityHotWaterTotal");
        Map(m => m.VoltageInverter).Index(38).Name("VoltageInverter");
        Map(m => m.WaterVolumeCurrent).Index(39).Name("WaterVolumeCurrent");
    }
}

