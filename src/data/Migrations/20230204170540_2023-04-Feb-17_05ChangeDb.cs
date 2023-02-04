using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace StiebelEltronDashboard.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202304Feb1705ChangeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodEnd",
                table: "HeatPumpDataPerPeriods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodStart",
                table: "HeatPumpDataPerPeriods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodEnd",
                table: "HeatPumpDataPerPeriods");

            migrationBuilder.DropColumn(
                name: "PeriodStart",
                table: "HeatPumpDataPerPeriods");
        }
    }
}
