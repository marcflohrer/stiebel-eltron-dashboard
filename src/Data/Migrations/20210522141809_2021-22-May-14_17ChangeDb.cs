using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StiebelEltronDashboard.Data.Migrations
{
    public partial class _202122May14_17ChangeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeatPumpData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPowerConsumption = table.Column<double>(type: "float", nullable: false),
                    ActualSpeedDensifier = table.Column<double>(type: "float", nullable: false),
                    AntiFreezeTemperature = table.Column<double>(type: "float", nullable: false),
                    CompressorInletTemperature = table.Column<double>(type: "float", nullable: false),
                    CondenserTemperature = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    DefrostStarts = table.Column<double>(type: "float", nullable: false),
                    DefrostTime = table.Column<double>(type: "float", nullable: false),
                    EvaporatorTemperature = table.Column<double>(type: "float", nullable: false),
                    ExhaustAirTemperature = table.Column<double>(type: "float", nullable: false),
                    FanPowerRel = table.Column<double>(type: "float", nullable: false),
                    HighPressure = table.Column<double>(type: "float", nullable: false),
                    HotGasTemperature = table.Column<double>(type: "float", nullable: false),
                    InletTemperature = table.Column<double>(type: "float", nullable: false),
                    IntermediateInjectionTemperature = table.Column<double>(type: "float", nullable: false),
                    LowPressure = table.Column<double>(type: "float", nullable: false),
                    OilSumpTemperature = table.Column<double>(type: "float", nullable: false),
                    OutdoorTemperature = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingDay = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingSum = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterDay = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterSum = table.Column<double>(type: "float", nullable: false),
                    PressureMedium = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages1 = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages2 = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHeatingSum = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHotWaterTotal = table.Column<double>(type: "float", nullable: false),
                    ReturnTemperature = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerDefrost = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHeating = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHotWater = table.Column<double>(type: "float", nullable: false),
                    SettingSpeedCompressed = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingDay = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingTotal = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterDay = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterTotal = table.Column<double>(type: "float", nullable: false),
                    VoltageInverter = table.Column<double>(type: "float", nullable: false),
                    WaterVolumeCurrent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatPumpData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeatPumpDataPerPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    ReturnTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    ReturnTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    InletTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    InletTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    InletTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    AntiFreezeTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    AntiFreezeTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    AntiFreezeTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    OutdoorTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    OutdoorTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    OutdoorTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    ExhaustAirTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    ExhaustAirTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    ExhaustAirTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    EvaporatorTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    EvaporatorTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    EvaporatorTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    TotalPowerConsumptionStart = table.Column<double>(type: "float", nullable: false),
                    TotalPowerConsumptionEnd = table.Column<double>(type: "float", nullable: false),
                    TotalPowerConsumptionDelta = table.Column<double>(type: "float", nullable: false),
                    CompressorInletTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    CompressorInletTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    CompressorInletTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    IntermediateInjectionTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    IntermediateInjectionTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    IntermediateInjectionTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    HotGasTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    HotGasTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    HotGasTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    CondenserTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    CondenserTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    CondenserTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    OilSumpTemperatureMin = table.Column<double>(type: "float", nullable: false),
                    OilSumpTemperatureMax = table.Column<double>(type: "float", nullable: false),
                    OilSumpTemperatureAverage = table.Column<double>(type: "float", nullable: false),
                    LowPressureMin = table.Column<double>(type: "float", nullable: false),
                    LowPressureMax = table.Column<double>(type: "float", nullable: false),
                    LowPressureAverage = table.Column<double>(type: "float", nullable: false),
                    PressureMediumMin = table.Column<double>(type: "float", nullable: false),
                    PressureMediumMax = table.Column<double>(type: "float", nullable: false),
                    PressureMediumAverage = table.Column<double>(type: "float", nullable: false),
                    HighPressureMin = table.Column<double>(type: "float", nullable: false),
                    HighPressureMax = table.Column<double>(type: "float", nullable: false),
                    HighPressureAverage = table.Column<double>(type: "float", nullable: false),
                    WaterVolumeCurrentMin = table.Column<double>(type: "float", nullable: false),
                    WaterVolumeCurrentMax = table.Column<double>(type: "float", nullable: false),
                    WaterVolumeCurrentAverage = table.Column<double>(type: "float", nullable: false),
                    VoltageInverterMin = table.Column<double>(type: "float", nullable: false),
                    VoltageInverterMax = table.Column<double>(type: "float", nullable: false),
                    VoltageInverterAverage = table.Column<double>(type: "float", nullable: false),
                    ActualSpeedDensifierMin = table.Column<double>(type: "float", nullable: false),
                    ActualSpeedDensifierMax = table.Column<double>(type: "float", nullable: false),
                    ActualSpeedDensifierAverage = table.Column<double>(type: "float", nullable: false),
                    SettingSpeedCompressedMin = table.Column<double>(type: "float", nullable: false),
                    SettingSpeedCompressedMax = table.Column<double>(type: "float", nullable: false),
                    SettingSpeedCompressedAverage = table.Column<double>(type: "float", nullable: false),
                    FanPowerRelMin = table.Column<double>(type: "float", nullable: false),
                    FanPowerRelMax = table.Column<double>(type: "float", nullable: false),
                    FanPowerRelAverage = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingDayMin = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingDayMax = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingDayAverage = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingTotalStart = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingTotalEnd = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHeatingTotalDelta = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterDayMin = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterDayMax = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterDayAverage = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterTotalStart = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterTotalEnd = table.Column<double>(type: "float", nullable: false),
                    VaporizerHeatQuantityHotWaterTotalDelta = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHeatingSumStart = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHeatingSumEnd = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHeatingSumDelta = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHotWaterTotalStart = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHotWaterTotalEnd = table.Column<double>(type: "float", nullable: false),
                    ReheatingStagesHeatQuantityHotWaterTotalDelta = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingDayMin = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingDayMax = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingDayAverage = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingSumStart = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingSumEnd = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHeatingSumDelta = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterDayMin = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterDayMax = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterDayAverage = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterSumStart = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterSumEnd = table.Column<double>(type: "float", nullable: false),
                    PowerConsumptionHotWaterSumDelta = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHeatingStart = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHeatingEnd = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHeatingDelta = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHotWaterStart = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHotWaterEnd = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerHotWaterDelta = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerDefrostStart = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerDefrostEnd = table.Column<double>(type: "float", nullable: false),
                    RuntimeVaporizerDefrostDelta = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages1Start = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages1End = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages1Delta = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages2Start = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages2End = table.Column<double>(type: "float", nullable: false),
                    ReheatingStages2Delta = table.Column<double>(type: "float", nullable: false),
                    DefrostTimeStart = table.Column<double>(type: "float", nullable: false),
                    DefrostTimeEnd = table.Column<double>(type: "float", nullable: false),
                    DefrostTimeDelta = table.Column<double>(type: "float", nullable: false),
                    DefrostStartsStart = table.Column<double>(type: "float", nullable: false),
                    DefrostStartsEnd = table.Column<double>(type: "float", nullable: false),
                    DefrostStartsDelta = table.Column<double>(type: "float", nullable: false),
                    Year = table.Column<double>(type: "float", nullable: false),
                    PeriodKind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodNumber = table.Column<int>(type: "int", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    First = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    Last = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatPumpDataPerPeriods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "Id1",
                table: "HeatPumpData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "Id",
                table: "HeatPumpDataPerPeriods",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeatPumpData");

            migrationBuilder.DropTable(
                name: "HeatPumpDataPerPeriods");
        }
    }
}
