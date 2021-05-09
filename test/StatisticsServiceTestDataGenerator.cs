using System;
using System.Collections;
using System.Collections.Generic;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServerTests {
    public class StatisticsServiceTestDailyPeriodDataGenerator : IEnumerable<object[]> {

        public static IEnumerable<object[]> GetHeatPumpTestData()
        {
            return _data;
        }
        private static readonly List<object[]> _data = new List<object[]> {
            new object[]
            {
                new List<HeatPumpDatum> () {
                    new HeatPumpDatum ().SetDoubles (1).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (14))),
                    new HeatPumpDatum ().SetDoubles (2).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (13))),
                    new HeatPumpDatum ().SetDoubles (3).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (12))),
                    new HeatPumpDatum ().SetDoubles (4).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (11))),
                    new HeatPumpDatum ().SetDoubles (5).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (10))),
                    new HeatPumpDatum ().SetDoubles (6).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (9))),
                    new HeatPumpDatum ().SetDoubles (7).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (8))),
                    new HeatPumpDatum ().SetDoubles (8).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (7))),
                    new HeatPumpDatum ().SetDoubles (9).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (6))),
                    new HeatPumpDatum ().SetDoubles (10).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (5))),
                    new HeatPumpDatum ().SetDoubles (11).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (4))),
                    new HeatPumpDatum ().SetDoubles (12).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (3))),
                    new HeatPumpDatum ().SetDoubles (13).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (2))),
                    new HeatPumpDatum ().SetDoubles (14).SetDateTimes (new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (1)))
                },
                new HeatPumpDataPerPeriod {
                    ReturnTemperatureMin = 1,
                    ReturnTemperatureMax = 14,
                    ReturnTemperatureAverage = 7.5,
                    InletTemperatureMin = 1,
                    InletTemperatureMax = 14,
                    InletTemperatureAverage = 7.5,
                    AntiFreezeTemperatureMin = 1,
                    AntiFreezeTemperatureMax = 14,
                    AntiFreezeTemperatureAverage = 7.5,
                    OutdoorTemperatureMin = 1,
                    OutdoorTemperatureMax = 14,
                    OutdoorTemperatureAverage = 7.5,
                    ExhaustAirTemperatureMin = 1,
                    ExhaustAirTemperatureMax = 14,
                    ExhaustAirTemperatureAverage = 7.5,
                    EvaporatorTemperatureMin = 1,
                    EvaporatorTemperatureMax = 14,
                    EvaporatorTemperatureAverage = 7.5,
                    TotalPowerConsumptionStart = 1,
                    TotalPowerConsumptionEnd = 14,
                    TotalPowerConsumptionDelta = 13,
                    CompressorInletTemperatureMin = 1,
                    CompressorInletTemperatureMax = 14,
                    CompressorInletTemperatureAverage = 7.5,
                    IntermediateInjectionTemperatureMin = 1,
                    IntermediateInjectionTemperatureMax = 14,
                    IntermediateInjectionTemperatureAverage = 7.5,
                    HotGasTemperatureMin = 1,
                    HotGasTemperatureMax = 14,
                    HotGasTemperatureAverage = 7.5,
                    CondenserTemperatureMin = 1,
                    CondenserTemperatureMax = 14,
                    CondenserTemperatureAverage = 7.5,
                    OilSumpTemperatureMin = 1,
                    OilSumpTemperatureMax = 14,
                    OilSumpTemperatureAverage = 7.5,
                    LowPressureMin = 1,
                    LowPressureMax = 14,
                    LowPressureAverage = 7.5,
                    PressureMediumMin = 1,
                    PressureMediumMax = 14,
                    PressureMediumAverage = 7.5,
                    HighPressureMin = 1,
                    HighPressureMax = 14,
                    HighPressureAverage = 7.5,
                    WaterVolumeCurrentMin = 1,
                    WaterVolumeCurrentMax = 14,
                    WaterVolumeCurrentAverage = 7.5,
                    VoltageInverterMin = 1,
                    VoltageInverterMax = 14,
                    VoltageInverterAverage = 7.5,
                    ActualSpeedDensifierMin = 1,
                    ActualSpeedDensifierMax = 14,
                    ActualSpeedDensifierAverage = 7.5,
                    SettingSpeedCompressedMin = 1,
                    SettingSpeedCompressedMax = 14,
                    SettingSpeedCompressedAverage = 7.5,
                    FanPowerRelMin = 1,
                    FanPowerRelMax = 14,
                    FanPowerRelAverage = 7.5,
                    VaporizerHeatQuantityHeatingDayStart = 1,
                    VaporizerHeatQuantityHeatingDayEnd = 14,
                    VaporizerHeatQuantityHeatingDayDelta = 13,
                    VaporizerHeatQuantityHeatingTotalStart = 1,
                    VaporizerHeatQuantityHeatingTotalEnd = 14,
                    VaporizerHeatQuantityHeatingTotalDelta = 13,
                    VaporizerHeatQuantityHotWaterDayStart = 1,
                    VaporizerHeatQuantityHotWaterDayEnd = 14,
                    VaporizerHeatQuantityHotWaterDayDelta = 13,
                    VaporizerHeatQuantityHotWaterTotalStart = 1,
                    VaporizerHeatQuantityHotWaterTotalEnd = 14,
                    VaporizerHeatQuantityHotWaterTotalDelta = 13,
                    ReheatingStagesHeatQuantityHeatingSumStart = 1,
                    ReheatingStagesHeatQuantityHeatingSumEnd = 14,
                    ReheatingStagesHeatQuantityHeatingSumDelta = 13,
                    ReheatingStagesHeatQuantityHotWaterTotalStart = 1,
                    ReheatingStagesHeatQuantityHotWaterTotalEnd = 14,
                    ReheatingStagesHeatQuantityHotWaterTotalDelta = 13,
                    PowerConsumptionHeatingDayStart = 1,
                    PowerConsumptionHeatingDayEnd = 14,
                    PowerConsumptionHeatingDayDelta = 13,
                    PowerConsumptionHeatingSumStart = 1,
                    PowerConsumptionHeatingSumEnd = 14,
                    PowerConsumptionHeatingSumDelta = 13,
                    PowerConsumptionHotWaterDayStart = 1,
                    PowerConsumptionHotWaterDayEnd = 14,
                    PowerConsumptionHotWaterDayDelta = 13,
                    PowerConsumptionHotWaterSumStart = 1,
                    PowerConsumptionHotWaterSumEnd = 14,
                    PowerConsumptionHotWaterSumDelta = 13,
                    RuntimeVaporizerHeatingStart = 1,
                    RuntimeVaporizerHeatingEnd = 14,
                    RuntimeVaporizerHeatingDelta = 13,
                    RuntimeVaporizerHotWaterStart = 1,
                    RuntimeVaporizerHotWaterEnd = 14,
                    RuntimeVaporizerHotWaterDelta = 13,
                    RuntimeVaporizerDefrostStart = 1,
                    RuntimeVaporizerDefrostEnd = 14,
                    RuntimeVaporizerDefrostDelta = 13,
                    ReheatingStages1Start = 1,
                    ReheatingStages1End = 14,
                    ReheatingStages1Delta = 13,
                    ReheatingStages2Start = 1,
                    ReheatingStages2End = 14,
                    ReheatingStages2Delta = 13,
                    DefrostTimeStart = 1,
                    DefrostTimeEnd = 14,
                    DefrostTimeDelta = 13,
                    DefrostStartsStart = 1,
                    DefrostStartsEnd = 14,
                    DefrostStartsDelta = 13,
                    Year = DateTime.Now.Year,
                    PeriodKind = "Day",
                    PeriodNumber = 13,
                    First = new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (14)),
                    Last = new DateTime(2021,5,1).Subtract (TimeSpan.FromHours (1)),
                }                
            }
        };

        public IEnumerator<object[]> GetEnumerator () => _data.GetEnumerator ();

        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator ();
    }
}