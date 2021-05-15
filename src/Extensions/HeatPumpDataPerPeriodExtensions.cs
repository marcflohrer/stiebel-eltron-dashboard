using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Extensions {
    public static class HeatPumpDataPerPeriodExtensions {
        public static double? PerformanceFactorPeriod (this HeatPumpDataPerPeriod heatPumpDataPerPeriod) 
        {
            var heatQuantityProducedInPeriod = heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingDayMax - heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingDayMin;
            var hotWaterProducedInPeriod = heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterDayMax - heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterDayMin;
            var powerConsumedForHeat = heatPumpDataPerPeriod.PowerConsumptionHeatingDayMax - heatPumpDataPerPeriod.PowerConsumptionHeatingDayMin;
            var powerConsumedForHotWater = heatPumpDataPerPeriod.PowerConsumptionHotWaterDayMax - heatPumpDataPerPeriod.PowerConsumptionHotWaterDayMin;
            return (heatQuantityProducedInPeriod + hotWaterProducedInPeriod) / (powerConsumedForHeat + powerConsumedForHotWater);
         }

        public static double? PerformanceFactorTotal (this HeatPumpDataPerPeriod heatPumpDataPerPeriod) 
        {
            var heatQuantityProducedInTotal = heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingTotalEnd - heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterTotalEnd;
            var hotWaterProducedInTotal = heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingTotalEnd - heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterTotalEnd;
            var powerConsumedForHeat = heatPumpDataPerPeriod.PowerConsumptionHeatingSumEnd - heatPumpDataPerPeriod.PowerConsumptionHotWaterSumEnd;
            var powerConsumedForHotWater = heatPumpDataPerPeriod.PowerConsumptionHotWaterDayMax - heatPumpDataPerPeriod.PowerConsumptionHotWaterDayMin;
            return (heatQuantityProducedInTotal + hotWaterProducedInTotal) / (powerConsumedForHeat + powerConsumedForHotWater);
         } 

        public static HeatPumpDataPerPeriod UpdateWith (this HeatPumpDataPerPeriod heatPumpDataPerPeriod, HeatPumpDataPerPeriod update) {
            heatPumpDataPerPeriod.ReturnTemperatureMin = update.ReturnTemperatureMin;
            heatPumpDataPerPeriod.ReturnTemperatureMax = update.ReturnTemperatureMax;
            heatPumpDataPerPeriod.ReturnTemperatureAverage = update.ReturnTemperatureAverage;
            heatPumpDataPerPeriod.InletTemperatureMin = update.InletTemperatureMin;
            heatPumpDataPerPeriod.InletTemperatureMax = update.InletTemperatureMax;
            heatPumpDataPerPeriod.InletTemperatureAverage = update.InletTemperatureAverage;
            heatPumpDataPerPeriod.AntiFreezeTemperatureMin = update.AntiFreezeTemperatureMin;
            heatPumpDataPerPeriod.AntiFreezeTemperatureMax = update.AntiFreezeTemperatureMax;
            heatPumpDataPerPeriod.AntiFreezeTemperatureAverage = update.AntiFreezeTemperatureAverage;
            heatPumpDataPerPeriod.OutdoorTemperatureMin = update.OutdoorTemperatureMin;
            heatPumpDataPerPeriod.OutdoorTemperatureMax = update.OutdoorTemperatureMax;
            heatPumpDataPerPeriod.OutdoorTemperatureAverage = update.OutdoorTemperatureAverage;
            heatPumpDataPerPeriod.ExhaustAirTemperatureMin = update.ExhaustAirTemperatureMin;
            heatPumpDataPerPeriod.ExhaustAirTemperatureMax = update.ExhaustAirTemperatureMax;
            heatPumpDataPerPeriod.ExhaustAirTemperatureAverage = update.ExhaustAirTemperatureAverage;
            heatPumpDataPerPeriod.EvaporatorTemperatureMin = update.EvaporatorTemperatureMin;
            heatPumpDataPerPeriod.EvaporatorTemperatureMax = update.EvaporatorTemperatureMax;
            heatPumpDataPerPeriod.EvaporatorTemperatureAverage = update.EvaporatorTemperatureAverage;
            heatPumpDataPerPeriod.TotalPowerConsumptionStart = update.TotalPowerConsumptionStart;
            heatPumpDataPerPeriod.TotalPowerConsumptionEnd = update.TotalPowerConsumptionEnd;
            heatPumpDataPerPeriod.TotalPowerConsumptionDelta = update.TotalPowerConsumptionDelta;
            heatPumpDataPerPeriod.CompressorInletTemperatureMin = update.CompressorInletTemperatureMin;
            heatPumpDataPerPeriod.CompressorInletTemperatureMax = update.CompressorInletTemperatureMax;
            heatPumpDataPerPeriod.CompressorInletTemperatureAverage = update.CompressorInletTemperatureAverage;
            heatPumpDataPerPeriod.IntermediateInjectionTemperatureMin = update.IntermediateInjectionTemperatureMin;
            heatPumpDataPerPeriod.IntermediateInjectionTemperatureMax = update.IntermediateInjectionTemperatureMax;
            heatPumpDataPerPeriod.IntermediateInjectionTemperatureAverage = update.IntermediateInjectionTemperatureAverage;
            heatPumpDataPerPeriod.HotGasTemperatureMin = update.HotGasTemperatureMin;
            heatPumpDataPerPeriod.HotGasTemperatureMax = update.HotGasTemperatureMax;
            heatPumpDataPerPeriod.HotGasTemperatureAverage = update.HotGasTemperatureAverage;
            heatPumpDataPerPeriod.CondenserTemperatureMin = update.CondenserTemperatureMin;
            heatPumpDataPerPeriod.CondenserTemperatureMax = update.CondenserTemperatureMax;
            heatPumpDataPerPeriod.CondenserTemperatureAverage = update.CondenserTemperatureAverage;
            heatPumpDataPerPeriod.OilSumpTemperatureMin = update.OilSumpTemperatureMin;
            heatPumpDataPerPeriod.OilSumpTemperatureMax = update.OilSumpTemperatureMax;
            heatPumpDataPerPeriod.OilSumpTemperatureAverage = update.OilSumpTemperatureAverage;
            heatPumpDataPerPeriod.LowPressureMin = update.LowPressureMin;
            heatPumpDataPerPeriod.LowPressureMax = update.LowPressureMax;
            heatPumpDataPerPeriod.LowPressureAverage = update.LowPressureAverage;
            heatPumpDataPerPeriod.PressureMediumMin = update.PressureMediumMin;
            heatPumpDataPerPeriod.PressureMediumMax = update.PressureMediumMax;
            heatPumpDataPerPeriod.PressureMediumAverage = update.PressureMediumAverage;
            heatPumpDataPerPeriod.HighPressureMin = update.HighPressureMin;
            heatPumpDataPerPeriod.HighPressureMax = update.HighPressureMax;
            heatPumpDataPerPeriod.HighPressureAverage = update.HighPressureAverage;
            heatPumpDataPerPeriod.WaterVolumeCurrentMin = update.WaterVolumeCurrentMin;
            heatPumpDataPerPeriod.WaterVolumeCurrentMax = update.WaterVolumeCurrentMax;
            heatPumpDataPerPeriod.WaterVolumeCurrentAverage = update.WaterVolumeCurrentAverage;
            heatPumpDataPerPeriod.VoltageInverterMin = update.VoltageInverterMin;
            heatPumpDataPerPeriod.VoltageInverterMax = update.VoltageInverterMax;
            heatPumpDataPerPeriod.VoltageInverterAverage = update.VoltageInverterAverage;
            heatPumpDataPerPeriod.ActualSpeedDensifierMin = update.ActualSpeedDensifierMin;
            heatPumpDataPerPeriod.ActualSpeedDensifierMax = update.ActualSpeedDensifierMax;
            heatPumpDataPerPeriod.ActualSpeedDensifierAverage = update.ActualSpeedDensifierAverage;
            heatPumpDataPerPeriod.SettingSpeedCompressedMin = update.SettingSpeedCompressedMin;
            heatPumpDataPerPeriod.SettingSpeedCompressedMax = update.SettingSpeedCompressedMax;
            heatPumpDataPerPeriod.SettingSpeedCompressedAverage = update.SettingSpeedCompressedAverage;
            heatPumpDataPerPeriod.FanPowerRelMin = update.FanPowerRelMin;
            heatPumpDataPerPeriod.FanPowerRelMax = update.FanPowerRelMax;
            heatPumpDataPerPeriod.FanPowerRelAverage = update.FanPowerRelAverage;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingDayMin = update.VaporizerHeatQuantityHeatingDayMin;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingDayMax = update.VaporizerHeatQuantityHeatingDayMax;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingDayAverage = update.VaporizerHeatQuantityHeatingDayAverage;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingTotalStart = update.VaporizerHeatQuantityHeatingTotalStart;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingTotalEnd = update.VaporizerHeatQuantityHeatingTotalEnd;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHeatingTotalDelta = update.VaporizerHeatQuantityHeatingTotalDelta;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterDayMin = update.VaporizerHeatQuantityHotWaterDayMin;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterDayMax = update.VaporizerHeatQuantityHotWaterDayMax;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterDayAverage = update.VaporizerHeatQuantityHotWaterDayAverage;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterTotalStart = update.VaporizerHeatQuantityHotWaterTotalStart;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterTotalEnd = update.VaporizerHeatQuantityHotWaterTotalEnd;
            heatPumpDataPerPeriod.VaporizerHeatQuantityHotWaterTotalDelta = update.VaporizerHeatQuantityHotWaterTotalDelta;
            heatPumpDataPerPeriod.ReheatingStagesHeatQuantityHeatingSumStart = update.ReheatingStagesHeatQuantityHeatingSumStart;
            heatPumpDataPerPeriod.ReheatingStagesHeatQuantityHeatingSumEnd = update.ReheatingStagesHeatQuantityHeatingSumEnd;
            heatPumpDataPerPeriod.ReheatingStagesHeatQuantityHeatingSumDelta = update.ReheatingStagesHeatQuantityHeatingSumDelta;
            heatPumpDataPerPeriod.ReheatingStagesHeatQuantityHotWaterTotalStart = update.ReheatingStagesHeatQuantityHotWaterTotalStart;
            heatPumpDataPerPeriod.ReheatingStagesHeatQuantityHotWaterTotalEnd = update.ReheatingStagesHeatQuantityHotWaterTotalEnd;
            heatPumpDataPerPeriod.ReheatingStagesHeatQuantityHotWaterTotalDelta = update.ReheatingStagesHeatQuantityHotWaterTotalDelta;
            heatPumpDataPerPeriod.PowerConsumptionHeatingDayMin = update.PowerConsumptionHeatingDayMin;
            heatPumpDataPerPeriod.PowerConsumptionHeatingDayMax = update.PowerConsumptionHeatingDayMax;
            heatPumpDataPerPeriod.PowerConsumptionHeatingDayAverage = update.PowerConsumptionHeatingDayAverage;
            heatPumpDataPerPeriod.PowerConsumptionHeatingSumStart = update.PowerConsumptionHeatingSumStart;
            heatPumpDataPerPeriod.PowerConsumptionHeatingSumEnd = update.PowerConsumptionHeatingSumEnd;
            heatPumpDataPerPeriod.PowerConsumptionHeatingSumDelta = update.PowerConsumptionHeatingSumDelta;
            heatPumpDataPerPeriod.PowerConsumptionHotWaterDayMin = update.PowerConsumptionHotWaterDayMin;
            heatPumpDataPerPeriod.PowerConsumptionHotWaterDayMax = update.PowerConsumptionHotWaterDayMax;
            heatPumpDataPerPeriod.PowerConsumptionHotWaterDayAverage = update.PowerConsumptionHotWaterDayAverage;
            heatPumpDataPerPeriod.PowerConsumptionHotWaterSumStart = update.PowerConsumptionHotWaterSumStart;
            heatPumpDataPerPeriod.PowerConsumptionHotWaterSumEnd = update.PowerConsumptionHotWaterSumEnd;
            heatPumpDataPerPeriod.PowerConsumptionHotWaterSumDelta = update.PowerConsumptionHotWaterSumDelta;
            heatPumpDataPerPeriod.RuntimeVaporizerHeatingStart = update.RuntimeVaporizerHeatingStart;
            heatPumpDataPerPeriod.RuntimeVaporizerHeatingEnd = update.RuntimeVaporizerHeatingEnd;
            heatPumpDataPerPeriod.RuntimeVaporizerHeatingDelta = update.RuntimeVaporizerHeatingDelta;
            heatPumpDataPerPeriod.RuntimeVaporizerHotWaterStart = update.RuntimeVaporizerHotWaterStart;
            heatPumpDataPerPeriod.RuntimeVaporizerHotWaterEnd = update.RuntimeVaporizerHotWaterEnd;
            heatPumpDataPerPeriod.RuntimeVaporizerHotWaterDelta = update.RuntimeVaporizerHotWaterDelta;
            heatPumpDataPerPeriod.RuntimeVaporizerDefrostStart = update.RuntimeVaporizerDefrostStart;
            heatPumpDataPerPeriod.RuntimeVaporizerDefrostEnd = update.RuntimeVaporizerDefrostEnd;
            heatPumpDataPerPeriod.RuntimeVaporizerDefrostDelta = update.RuntimeVaporizerDefrostDelta;
            heatPumpDataPerPeriod.ReheatingStages1Start = update.ReheatingStages1Start;
            heatPumpDataPerPeriod.ReheatingStages1End = update.ReheatingStages1End;
            heatPumpDataPerPeriod.ReheatingStages1Delta = update.ReheatingStages1Delta;
            heatPumpDataPerPeriod.ReheatingStages2Start = update.ReheatingStages2Start;
            heatPumpDataPerPeriod.ReheatingStages2End = update.ReheatingStages2End;
            heatPumpDataPerPeriod.ReheatingStages2Delta = update.ReheatingStages2Delta;
            heatPumpDataPerPeriod.DefrostTimeStart = update.DefrostTimeStart;
            heatPumpDataPerPeriod.DefrostTimeEnd = update.DefrostTimeEnd;
            heatPumpDataPerPeriod.DefrostTimeDelta = update.DefrostTimeDelta;
            heatPumpDataPerPeriod.DefrostStartsStart = update.DefrostStartsStart;
            heatPumpDataPerPeriod.DefrostStartsEnd = update.DefrostStartsEnd;
            heatPumpDataPerPeriod.DefrostStartsDelta = update.DefrostStartsDelta;
            heatPumpDataPerPeriod.Year = update.Year;
            heatPumpDataPerPeriod.PeriodKind = update.PeriodKind;
            heatPumpDataPerPeriod.PeriodNumber = update.PeriodNumber;
            heatPumpDataPerPeriod.DateUpdated = update.DateUpdated;
            heatPumpDataPerPeriod.First = update.First;
            heatPumpDataPerPeriod.Last = update.Last;
            return heatPumpDataPerPeriod;
        }
    }
}