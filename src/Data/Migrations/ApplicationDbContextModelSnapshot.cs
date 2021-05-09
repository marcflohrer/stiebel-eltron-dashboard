﻿// <auto-generated />
﻿
/*
Copyright (c) .NET Foundation and Contributors

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

I (Marc Lohrer) changed the file.

This notice is intended to comply with the Apache Licence 2. 0 section 4.b. that states

"4. You may reproduce and distribute copies of the Work or Derivative Works thereof in any medium, 
 with or without modifications, and in Source or Object form, provided that You meet the following conditions:
 ... 
 b. You must cause any modified files to carry prominent notices stating that You changed the files; and
 "
*/
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StiebelEltronApiServer.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StiebelEltronApiServer.Models.HeatPumpDataPerPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ActualSpeedDensifierAverage")
                        .HasColumnType("float");

                    b.Property<double>("ActualSpeedDensifierMax")
                        .HasColumnType("float");

                    b.Property<double>("ActualSpeedDensifierMin")
                        .HasColumnType("float");

                    b.Property<double>("AntiFreezeTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("AntiFreezeTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("AntiFreezeTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("CompressorInletTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("CompressorInletTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("CompressorInletTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("CondenserTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("CondenserTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("CondenserTemperatureMin")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<double>("DefrostStartsDelta")
                        .HasColumnType("float");

                    b.Property<double>("DefrostStartsEnd")
                        .HasColumnType("float");

                    b.Property<double>("DefrostStartsStart")
                        .HasColumnType("float");

                    b.Property<double>("DefrostTimeDelta")
                        .HasColumnType("float");

                    b.Property<double>("DefrostTimeEnd")
                        .HasColumnType("float");

                    b.Property<double>("DefrostTimeStart")
                        .HasColumnType("float");

                    b.Property<double>("EvaporatorTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("EvaporatorTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("EvaporatorTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("ExhaustAirTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("ExhaustAirTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("ExhaustAirTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("FanPowerRelAverage")
                        .HasColumnType("float");

                    b.Property<double>("FanPowerRelMax")
                        .HasColumnType("float");

                    b.Property<double>("FanPowerRelMin")
                        .HasColumnType("float");

                    b.Property<DateTime>("First")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<double>("HighPressureAverage")
                        .HasColumnType("float");

                    b.Property<double>("HighPressureMax")
                        .HasColumnType("float");

                    b.Property<double>("HighPressureMin")
                        .HasColumnType("float");

                    b.Property<double>("HotGasTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("HotGasTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("HotGasTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("InletTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("InletTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("InletTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("IntermediateInjectionTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("IntermediateInjectionTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("IntermediateInjectionTemperatureMin")
                        .HasColumnType("float");

                    b.Property<DateTime>("Last")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<double>("LowPressureAverage")
                        .HasColumnType("float");

                    b.Property<double>("LowPressureMax")
                        .HasColumnType("float");

                    b.Property<double>("LowPressureMin")
                        .HasColumnType("float");

                    b.Property<double>("OilSumpTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("OilSumpTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("OilSumpTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("OutdoorTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("OutdoorTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("OutdoorTemperatureMin")
                        .HasColumnType("float");

                    b.Property<string>("PeriodKind")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeriodNumber")
                        .HasColumnType("int");

                    b.Property<double>("PowerConsumptionHeatingDayDelta")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingDayEnd")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingDayStart")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingSumDelta")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingSumEnd")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingSumStart")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterDayDelta")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterDayEnd")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterDayStart")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterSumDelta")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterSumEnd")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterSumStart")
                        .HasColumnType("float");

                    b.Property<double>("PressureMediumAverage")
                        .HasColumnType("float");

                    b.Property<double>("PressureMediumMax")
                        .HasColumnType("float");

                    b.Property<double>("PressureMediumMin")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages1Delta")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages1End")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages1Start")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages2Delta")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages2End")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages2Start")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHeatingSumDelta")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHeatingSumEnd")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHeatingSumStart")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHotWaterTotalDelta")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHotWaterTotalEnd")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHotWaterTotalStart")
                        .HasColumnType("float");

                    b.Property<double>("ReturnTemperatureAverage")
                        .HasColumnType("float");

                    b.Property<double>("ReturnTemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("ReturnTemperatureMin")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerDefrostDelta")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerDefrostEnd")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerDefrostStart")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHeatingDelta")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHeatingEnd")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHeatingStart")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHotWaterDelta")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHotWaterEnd")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHotWaterStart")
                        .HasColumnType("float");

                    b.Property<double>("SettingSpeedCompressedAverage")
                        .HasColumnType("float");

                    b.Property<double>("SettingSpeedCompressedMax")
                        .HasColumnType("float");

                    b.Property<double>("SettingSpeedCompressedMin")
                        .HasColumnType("float");

                    b.Property<double>("TotalPowerConsumptionDelta")
                        .HasColumnType("float");

                    b.Property<double>("TotalPowerConsumptionEnd")
                        .HasColumnType("float");

                    b.Property<double>("TotalPowerConsumptionStart")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingDayDelta")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingDayEnd")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingDayStart")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingTotalDelta")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingTotalEnd")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingTotalStart")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterDayDelta")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterDayEnd")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterDayStart")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterTotalDelta")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterTotalEnd")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterTotalStart")
                        .HasColumnType("float");

                    b.Property<double>("VoltageInverterAverage")
                        .HasColumnType("float");

                    b.Property<double>("VoltageInverterMax")
                        .HasColumnType("float");

                    b.Property<double>("VoltageInverterMin")
                        .HasColumnType("float");

                    b.Property<double>("WaterVolumeCurrentAverage")
                        .HasColumnType("float");

                    b.Property<double>("WaterVolumeCurrentMax")
                        .HasColumnType("float");

                    b.Property<double>("WaterVolumeCurrentMin")
                        .HasColumnType("float");

                    b.Property<double>("Year")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id");

                    b.ToTable("HeatPumpDataPerPeriods");
                });

            modelBuilder.Entity("StiebelEltronApiServer.Models.HeatPumpDatum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ActualSpeedDensifier")
                        .HasColumnType("float");

                    b.Property<double>("AntiFreezeTemperature")
                        .HasColumnType("float");

                    b.Property<double>("CompressorInletTemperature")
                        .HasColumnType("float");

                    b.Property<double>("CondenserTemperature")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<double>("DefrostStarts")
                        .HasColumnType("float");

                    b.Property<double>("DefrostTime")
                        .HasColumnType("float");

                    b.Property<double>("EvaporatorTemperature")
                        .HasColumnType("float");

                    b.Property<double>("ExhaustAirTemperature")
                        .HasColumnType("float");

                    b.Property<double>("FanPowerRel")
                        .HasColumnType("float");

                    b.Property<double>("HighPressure")
                        .HasColumnType("float");

                    b.Property<double>("HotGasTemperature")
                        .HasColumnType("float");

                    b.Property<double>("InletTemperature")
                        .HasColumnType("float");

                    b.Property<double>("IntermediateInjectionTemperature")
                        .HasColumnType("float");

                    b.Property<double>("LowPressure")
                        .HasColumnType("float");

                    b.Property<double>("OilSumpTemperature")
                        .HasColumnType("float");

                    b.Property<double>("OutdoorTemperature")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingDay")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHeatingSum")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterDay")
                        .HasColumnType("float");

                    b.Property<double>("PowerConsumptionHotWaterSum")
                        .HasColumnType("float");

                    b.Property<double>("PressureMedium")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages1")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStages2")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHeatingSum")
                        .HasColumnType("float");

                    b.Property<double>("ReheatingStagesHeatQuantityHotWaterTotal")
                        .HasColumnType("float");

                    b.Property<double>("ReturnTemperature")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerDefrost")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHeating")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeVaporizerHotWater")
                        .HasColumnType("float");

                    b.Property<double>("SettingSpeedCompressed")
                        .HasColumnType("float");

                    b.Property<double>("TotalPowerConsumption")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingDay")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHeatingTotal")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterDay")
                        .HasColumnType("float");

                    b.Property<double>("VaporizerHeatQuantityHotWaterTotal")
                        .HasColumnType("float");

                    b.Property<double>("VoltageInverter")
                        .HasColumnType("float");

                    b.Property<double>("WaterVolumeCurrent")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "Id1");

                    b.ToTable("HeatPumpData");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StiebelEltronApiServer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StiebelEltronApiServer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StiebelEltronApiServer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("StiebelEltronApiServer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
