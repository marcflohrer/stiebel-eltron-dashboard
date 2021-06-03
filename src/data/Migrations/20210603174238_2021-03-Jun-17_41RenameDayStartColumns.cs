using Microsoft.EntityFrameworkCore.Migrations;

namespace StiebelEltronDashboard.Data.Migrations
{
    public partial class _202103Jun17_41RenameDayStartColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHotWaterDayMin",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHotWaterDayStart");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHotWaterDayMax",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHotWaterDayEnd");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHotWaterDayAverage",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHotWaterDayDelta");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHeatingDayMin",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHeatingDayStart");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHeatingDayMax",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHeatingDayEnd");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHeatingDayAverage",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHeatingDayDelta");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHotWaterDayMin",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHotWaterDayStart");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHotWaterDayMax",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHotWaterDayEnd");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHotWaterDayAverage",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHotWaterDayDelta");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHeatingDayMin",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHeatingDayStart");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHeatingDayMax",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHeatingDayEnd");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHeatingDayAverage",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHeatingDayDelta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHotWaterDayStart",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHotWaterDayMin");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHotWaterDayEnd",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHotWaterDayMax");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHotWaterDayDelta",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHotWaterDayAverage");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHeatingDayStart",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHeatingDayMin");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHeatingDayEnd",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHeatingDayMax");

            migrationBuilder.RenameColumn(
                name: "VaporizerHeatQuantityHeatingDayDelta",
                table: "HeatPumpDataPerPeriods",
                newName: "VaporizerHeatQuantityHeatingDayAverage");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHotWaterDayStart",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHotWaterDayMin");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHotWaterDayEnd",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHotWaterDayMax");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHotWaterDayDelta",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHotWaterDayAverage");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHeatingDayStart",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHeatingDayMin");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHeatingDayEnd",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHeatingDayMax");

            migrationBuilder.RenameColumn(
                name: "PowerConsumptionHeatingDayDelta",
                table: "HeatPumpDataPerPeriods",
                newName: "PowerConsumptionHeatingDayAverage");
        }
    }
}
