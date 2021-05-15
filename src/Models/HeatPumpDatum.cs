using StiebelEltronDashboard.Models.Shared;
using System;

#nullable disable

namespace StiebelEltronDashboard.Models
{
    public partial class HeatPumpDatum : ValueObject
    {
        public int Id { get; set; }
        public double TotalPowerConsumption { get; set; }
        public double ActualSpeedDensifier { get; set; }
        public double AntiFreezeTemperature { get; set; }
        public double CompressorInletTemperature { get; set; }
        public double CondenserTemperature { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public double DefrostStarts { get; set; }
        public double DefrostTime { get; set; }
        public double EvaporatorTemperature { get; set; }
        public double ExhaustAirTemperature { get; set; }
        public double FanPowerRel { get; set; }
        public double HighPressure { get; set; }
        public double HotGasTemperature { get; set; }
        public double InletTemperature { get; set; }
        public double IntermediateInjectionTemperature { get; set; }
        public double LowPressure { get; set; }
        public double OilSumpTemperature { get; set; }
        public double OutdoorTemperature { get; set; }
        public double PowerConsumptionHeatingDay { get; set; }
        public double PowerConsumptionHeatingSum { get; set; }
        public double PowerConsumptionHotWaterDay { get; set; }
        public double PowerConsumptionHotWaterSum { get; set; }
        public double PressureMedium { get; set; }
        public double ReheatingStages1 { get; set; }
        public double ReheatingStages2 { get; set; }
        public double ReheatingStagesHeatQuantityHeatingSum { get; set; }
        public double ReheatingStagesHeatQuantityHotWaterTotal { get; set; }
        public double ReturnTemperature { get; set; }
        public double RuntimeVaporizerDefrost { get; set; }
        public double RuntimeVaporizerHeating { get; set; }
        public double RuntimeVaporizerHotWater { get; set; }
        public double SettingSpeedCompressed { get; set; }
        public double VaporizerHeatQuantityHeatingDay { get; set; }
        public double VaporizerHeatQuantityHeatingTotal { get; set; }
        public double VaporizerHeatQuantityHotWaterDay { get; set; }
        public double VaporizerHeatQuantityHotWaterTotal { get; set; }
        public double VoltageInverter { get; set; }
        public double WaterVolumeCurrent { get; set; }
    }
}
