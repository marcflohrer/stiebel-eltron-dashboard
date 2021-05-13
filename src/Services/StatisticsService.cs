using System;
using System.Collections.Generic;
using System.Linq;
using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Services {
    public class StatisticsService : IStatisticsService {
        public HeatPumpDataPerPeriod GetHeatPumpDataPerPeriod (IEnumerable<HeatPumpDatum> heatPumpData, int year, string periodKind, int periodNumber, DateTime now) {
            Console.WriteLine($"--> GetHeatPumpDataPerPeriod: heatPumpData.Count = {heatPumpData.Count()}, year={year}, period={periodKind}, periodNumber={periodNumber}, now={now.ToLongDateString()}");
            var result = new HeatPumpDataPerPeriod () {
                ReturnTemperatureMin = heatPumpData.GetMinForMetric (h => h.ReturnTemperature) ?? Double.MinValue,
                    ReturnTemperatureMax = heatPumpData.GetMaxForMetric (h => h.ReturnTemperature) ?? Double.MinValue,
                    ReturnTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.ReturnTemperature) ?? Double.MinValue,
                    InletTemperatureMin = heatPumpData.GetMinForMetric (h => h.InletTemperature) ?? Double.MinValue,
                    InletTemperatureMax = heatPumpData.GetMaxForMetric (h => h.InletTemperature) ?? Double.MinValue,
                    InletTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.InletTemperature) ?? Double.MinValue,
                    AntiFreezeTemperatureMin = heatPumpData.GetMinForMetric (h => h.AntiFreezeTemperature) ?? Double.MinValue,
                    AntiFreezeTemperatureMax = heatPumpData.GetMaxForMetric (h => h.AntiFreezeTemperature) ?? Double.MinValue,
                    AntiFreezeTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.AntiFreezeTemperature) ?? Double.MinValue,
                    OutdoorTemperatureMin = heatPumpData.GetMinForMetric (h => h.OutdoorTemperature) ?? Double.MinValue,
                    OutdoorTemperatureMax = heatPumpData.GetMaxForMetric (h => h.OutdoorTemperature) ?? Double.MinValue,
                    OutdoorTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.OutdoorTemperature) ?? Double.MinValue,
                    ExhaustAirTemperatureMin = heatPumpData.GetMinForMetric (h => h.ExhaustAirTemperature) ?? Double.MinValue,
                    ExhaustAirTemperatureMax = heatPumpData.GetMaxForMetric (h => h.ExhaustAirTemperature) ?? Double.MinValue,
                    ExhaustAirTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.ExhaustAirTemperature) ?? Double.MinValue,
                    EvaporatorTemperatureMin = heatPumpData.GetMinForMetric (h => h.EvaporatorTemperature) ?? Double.MinValue,
                    EvaporatorTemperatureMax = heatPumpData.GetMaxForMetric (h => h.EvaporatorTemperature) ?? Double.MinValue,
                    EvaporatorTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.EvaporatorTemperature) ?? Double.MinValue,
                    TotalPowerConsumptionStart = heatPumpData.GetStartForMetric (h => h.TotalPowerConsumption) ?? Double.MinValue,
                    TotalPowerConsumptionEnd = heatPumpData.GetEndForMetric (h => h.TotalPowerConsumption) ?? Double.MinValue,
                    TotalPowerConsumptionDelta = heatPumpData.GetDeltaForMetric (h => h.TotalPowerConsumption) ?? Double.MinValue,
                    CompressorInletTemperatureMin = heatPumpData.GetMinForMetric (h => h.CompressorInletTemperature) ?? Double.MinValue,
                    CompressorInletTemperatureMax = heatPumpData.GetMaxForMetric (h => h.CompressorInletTemperature) ?? Double.MinValue,
                    CompressorInletTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.CompressorInletTemperature) ?? Double.MinValue,
                    IntermediateInjectionTemperatureMin = heatPumpData.GetMinForMetric (h => h.IntermediateInjectionTemperature) ?? Double.MinValue,
                    IntermediateInjectionTemperatureMax = heatPumpData.GetMaxForMetric (h => h.IntermediateInjectionTemperature) ?? Double.MinValue,
                    IntermediateInjectionTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.IntermediateInjectionTemperature) ?? Double.MinValue,
                    HotGasTemperatureMin = heatPumpData.GetMinForMetric (h => h.HotGasTemperature) ?? Double.MinValue,
                    HotGasTemperatureMax = heatPumpData.GetMaxForMetric (h => h.HotGasTemperature) ?? Double.MinValue,
                    HotGasTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.HotGasTemperature) ?? Double.MinValue,
                    CondenserTemperatureMin = heatPumpData.GetMinForMetric (h => h.CondenserTemperature) ?? Double.MinValue,
                    CondenserTemperatureMax = heatPumpData.GetMaxForMetric (h => h.CondenserTemperature) ?? Double.MinValue,
                    CondenserTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.CondenserTemperature) ?? Double.MinValue,
                    OilSumpTemperatureMin = heatPumpData.GetMinForMetric (h => h.OilSumpTemperature) ?? Double.MinValue,
                    OilSumpTemperatureMax = heatPumpData.GetMaxForMetric (h => h.OilSumpTemperature)?? Double.MinValue,
                    OilSumpTemperatureAverage = heatPumpData.GetAverageForMetric (h => h.OilSumpTemperature)?? Double.MinValue,
                    LowPressureMin = heatPumpData.GetMinForMetric (h => h.LowPressure)?? Double.MinValue,
                    LowPressureMax = heatPumpData.GetMaxForMetric (h => h.LowPressure)?? Double.MinValue,
                    LowPressureAverage = heatPumpData.GetAverageForMetric (h => h.LowPressure)?? Double.MinValue,
                    PressureMediumMin = heatPumpData.GetMinForMetric (h => h.PressureMedium)?? Double.MinValue,
                    PressureMediumMax = heatPumpData.GetMaxForMetric (h => h.PressureMedium)?? Double.MinValue,
                    PressureMediumAverage = heatPumpData.GetAverageForMetric (h => h.PressureMedium)?? Double.MinValue,
                    HighPressureMin = heatPumpData.GetMinForMetric (h => h.HighPressure)?? Double.MinValue,
                    HighPressureMax = heatPumpData.GetMaxForMetric (h => h.HighPressure)?? Double.MinValue,
                    HighPressureAverage = heatPumpData.GetAverageForMetric (h => h.HighPressure)?? Double.MinValue,
                    WaterVolumeCurrentMin = heatPumpData.GetMinForMetric (h => h.WaterVolumeCurrent)?? Double.MinValue,
                    WaterVolumeCurrentMax = heatPumpData.GetMaxForMetric (h => h.WaterVolumeCurrent)?? Double.MinValue,
                    WaterVolumeCurrentAverage = heatPumpData.GetAverageForMetric (h => h.WaterVolumeCurrent)?? Double.MinValue,
                    VoltageInverterMin = heatPumpData.GetMinForMetric (h => h.VoltageInverter)?? Double.MinValue,
                    VoltageInverterMax = heatPumpData.GetMaxForMetric (h => h.VoltageInverter)?? Double.MinValue,
                    VoltageInverterAverage = heatPumpData.GetAverageForMetric (h => h.VoltageInverter)?? Double.MinValue,
                    ActualSpeedDensifierMin = heatPumpData.GetMinForMetric (h => h.ActualSpeedDensifier)?? Double.MinValue,
                    ActualSpeedDensifierMax = heatPumpData.GetMaxForMetric (h => h.ActualSpeedDensifier)?? Double.MinValue,
                    ActualSpeedDensifierAverage = heatPumpData.GetAverageForMetric (h => h.ActualSpeedDensifier)?? Double.MinValue,
                    SettingSpeedCompressedMin = heatPumpData.GetMinForMetric (h => h.SettingSpeedCompressed)?? Double.MinValue,
                    SettingSpeedCompressedMax = heatPumpData.GetMaxForMetric (h => h.SettingSpeedCompressed)?? Double.MinValue,
                    SettingSpeedCompressedAverage = heatPumpData.GetAverageForMetric (h => h.SettingSpeedCompressed)?? Double.MinValue,
                    FanPowerRelMin = heatPumpData.GetMinForMetric (h => h.FanPowerRel)?? Double.MinValue,
                    FanPowerRelMax = heatPumpData.GetMaxForMetric (h => h.FanPowerRel)?? Double.MinValue,
                    FanPowerRelAverage = heatPumpData.GetAverageForMetric (h => h.FanPowerRel)?? Double.MinValue,
                    VaporizerHeatQuantityHeatingDayStart = heatPumpData.GetStartForMetric (h => h.VaporizerHeatQuantityHeatingDay)?? Double.MinValue,
                    VaporizerHeatQuantityHeatingDayEnd = heatPumpData.GetEndForMetric (h => h.VaporizerHeatQuantityHeatingDay)?? Double.MinValue,
                    VaporizerHeatQuantityHeatingDayDelta = heatPumpData.GetDeltaForMetric (h => h.VaporizerHeatQuantityHeatingDay)?? Double.MinValue,
                    VaporizerHeatQuantityHeatingTotalStart = heatPumpData.GetStartForMetric (h => h.VaporizerHeatQuantityHeatingTotal)?? Double.MinValue,
                    VaporizerHeatQuantityHeatingTotalEnd = heatPumpData.GetEndForMetric (h => h.VaporizerHeatQuantityHeatingTotal)?? Double.MinValue,
                    VaporizerHeatQuantityHeatingTotalDelta = heatPumpData.GetDeltaForMetric (h => h.VaporizerHeatQuantityHeatingTotal)?? Double.MinValue,
                    VaporizerHeatQuantityHotWaterDayStart = heatPumpData.GetStartForMetric (h => h.VaporizerHeatQuantityHotWaterDay)?? Double.MinValue,
                    VaporizerHeatQuantityHotWaterDayEnd = heatPumpData.GetEndForMetric (h => h.VaporizerHeatQuantityHotWaterDay)?? Double.MinValue,
                    VaporizerHeatQuantityHotWaterDayDelta = heatPumpData.GetDeltaForMetric (h => h.VaporizerHeatQuantityHotWaterDay)?? Double.MinValue,
                    VaporizerHeatQuantityHotWaterTotalStart = heatPumpData.GetStartForMetric (h => h.VaporizerHeatQuantityHotWaterTotal)?? Double.MinValue,
                    VaporizerHeatQuantityHotWaterTotalEnd = heatPumpData.GetEndForMetric (h => h.VaporizerHeatQuantityHotWaterTotal)?? Double.MinValue,
                    VaporizerHeatQuantityHotWaterTotalDelta = heatPumpData.GetDeltaForMetric (h => h.VaporizerHeatQuantityHotWaterTotal)?? Double.MinValue,
                    ReheatingStagesHeatQuantityHeatingSumStart = heatPumpData.GetStartForMetric (h => h.ReheatingStagesHeatQuantityHeatingSum)?? Double.MinValue,
                    ReheatingStagesHeatQuantityHeatingSumEnd = heatPumpData.GetEndForMetric (h => h.ReheatingStagesHeatQuantityHeatingSum)?? Double.MinValue,
                    ReheatingStagesHeatQuantityHeatingSumDelta = heatPumpData.GetDeltaForMetric (h => h.ReheatingStagesHeatQuantityHeatingSum)?? Double.MinValue,
                    ReheatingStagesHeatQuantityHotWaterTotalStart = heatPumpData.GetStartForMetric (h => h.ReheatingStagesHeatQuantityHotWaterTotal)?? Double.MinValue,
                    ReheatingStagesHeatQuantityHotWaterTotalEnd = heatPumpData.GetEndForMetric (h => h.ReheatingStagesHeatQuantityHotWaterTotal)?? Double.MinValue,
                    ReheatingStagesHeatQuantityHotWaterTotalDelta = heatPumpData.GetDeltaForMetric (h => h.ReheatingStagesHeatQuantityHotWaterTotal)?? Double.MinValue,
                    PowerConsumptionHeatingDayStart = heatPumpData.GetStartForMetric (h => h.PowerConsumptionHeatingDay)?? Double.MinValue,
                    PowerConsumptionHeatingDayEnd = heatPumpData.GetEndForMetric (h => h.PowerConsumptionHeatingDay)?? Double.MinValue,
                    PowerConsumptionHeatingDayDelta = heatPumpData.GetDeltaForMetric (h => h.PowerConsumptionHeatingDay)?? Double.MinValue,
                    PowerConsumptionHeatingSumStart = heatPumpData.GetStartForMetric (h => h.PowerConsumptionHeatingSum)?? Double.MinValue,
                    PowerConsumptionHeatingSumEnd = heatPumpData.GetEndForMetric (h => h.PowerConsumptionHeatingSum)?? Double.MinValue,
                    PowerConsumptionHeatingSumDelta = heatPumpData.GetDeltaForMetric (h => h.PowerConsumptionHeatingSum)?? Double.MinValue,
                    PowerConsumptionHotWaterDayStart = heatPumpData.GetStartForMetric (h => h.PowerConsumptionHotWaterDay)?? Double.MinValue,
                    PowerConsumptionHotWaterDayEnd = heatPumpData.GetEndForMetric (h => h.PowerConsumptionHotWaterDay)?? Double.MinValue,
                    PowerConsumptionHotWaterDayDelta = heatPumpData.GetDeltaForMetric (h => h.PowerConsumptionHotWaterDay)?? Double.MinValue,
                    PowerConsumptionHotWaterSumStart = heatPumpData.GetStartForMetric (h => h.PowerConsumptionHotWaterSum)?? Double.MinValue,
                    PowerConsumptionHotWaterSumEnd = heatPumpData.GetEndForMetric (h => h.PowerConsumptionHotWaterSum)?? Double.MinValue,
                    PowerConsumptionHotWaterSumDelta = heatPumpData.GetDeltaForMetric (h => h.PowerConsumptionHotWaterSum)?? Double.MinValue,
                    RuntimeVaporizerHeatingStart = heatPumpData.GetStartForMetric (h => h.RuntimeVaporizerHeating)?? Double.MinValue,
                    RuntimeVaporizerHeatingEnd = heatPumpData.GetEndForMetric (h => h.RuntimeVaporizerHeating)?? Double.MinValue,
                    RuntimeVaporizerHeatingDelta = heatPumpData.GetDeltaForMetric (h => h.RuntimeVaporizerHeating)?? Double.MinValue,
                    RuntimeVaporizerHotWaterStart = heatPumpData.GetStartForMetric (h => h.RuntimeVaporizerHotWater)?? Double.MinValue,
                    RuntimeVaporizerHotWaterEnd = heatPumpData.GetEndForMetric (h => h.RuntimeVaporizerHotWater)?? Double.MinValue,
                    RuntimeVaporizerHotWaterDelta = heatPumpData.GetDeltaForMetric (h => h.RuntimeVaporizerHotWater)?? Double.MinValue,
                    RuntimeVaporizerDefrostStart = heatPumpData.GetStartForMetric (h => h.RuntimeVaporizerDefrost)?? Double.MinValue,
                    RuntimeVaporizerDefrostEnd = heatPumpData.GetEndForMetric (h => h.RuntimeVaporizerDefrost)?? Double.MinValue,
                    RuntimeVaporizerDefrostDelta = heatPumpData.GetDeltaForMetric (h => h.RuntimeVaporizerDefrost)?? Double.MinValue,
                    ReheatingStages1Start = heatPumpData.GetStartForMetric (h => h.ReheatingStages1)?? Double.MinValue,
                    ReheatingStages1End = heatPumpData.GetEndForMetric (h => h.ReheatingStages1)?? Double.MinValue,
                    ReheatingStages1Delta = heatPumpData.GetDeltaForMetric (h => h.ReheatingStages1)?? Double.MinValue,
                    ReheatingStages2Start = heatPumpData.GetStartForMetric (h => h.ReheatingStages2)?? Double.MinValue,
                    ReheatingStages2End = heatPumpData.GetEndForMetric (h => h.ReheatingStages2)?? Double.MinValue,
                    ReheatingStages2Delta = heatPumpData.GetDeltaForMetric (h => h.ReheatingStages2)?? Double.MinValue,
                    DefrostTimeStart = heatPumpData.GetStartForMetric (h => h.DefrostTime)?? Double.MinValue,
                    DefrostTimeEnd = heatPumpData.GetEndForMetric (h => h.DefrostTime)?? Double.MinValue,
                    DefrostTimeDelta = heatPumpData.GetDeltaForMetric (h => h.DefrostTime)?? Double.MinValue,
                    DefrostStartsStart = heatPumpData.GetStartForMetric (h => h.DefrostStarts)?? Double.MinValue,
                    DefrostStartsEnd = heatPumpData.GetEndForMetric (h => h.DefrostStarts)?? Double.MinValue,
                    DefrostStartsDelta = heatPumpData.GetDeltaForMetric (h => h.DefrostStarts)?? Double.MinValue,
                    Year = year,
                    PeriodKind = periodKind,
                    PeriodNumber = periodNumber,
                    First = heatPumpData.GetFirst (),
                    Last = heatPumpData.GetLast (),
                    DateUpdated = now,
                    DateCreated = now
            };
            Console.WriteLine($"<-- GetHeatPumpDataPerPeriod: heatPumpData.Count = {heatPumpData.Count()}, year={year}, period={periodKind}, periodNumber={periodNumber}, now={now.ToLongDateString()}");
            return result;
        }
    }
}