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
using StiebelEltronDashboard.Models;
using StiebelEltronDashboard.Repositories;
using StiebelEltronDashboard.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using StiebelEltronDashboard.Extensions;
using StiebelEltronDashboard.Services.HtmlServices;
using Serilog.Events;
using StiebelEltronDashboard.Jobs;
using Microsoft.OpenApi.Models;

namespace StiebelEltronDashboard
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new Exception("Database connection is not set.");
            }

            var logger = Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .Filter.With<LoggingFilter>()
                        .Enrich.FromLogContext()
                        .WriteTo.File("logs/dashboard.log", fileSizeLimitBytes: 1048576, rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                        .WriteTo.Console()
                        .CreateLogger();

            // DbContext pooling: AddDbContextPool enables pooling of DbContext instances. 
            // Context pooling can increase throughput in high-scale scenarios such as web servers by reusing context instances, 
            // rather than creating new instances for each request.
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddMvc();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backup/Restore API", Version = "v1" });
            });
            services.AddControllers();
            services.AddTransient<IXpathService, XpathService>()
                .AddSingleton(logger)
                .AddTransient<IHtmlScanner, HtmlScanner>()
                .AddTransient<ITidyUpDirtyHtml, TidyUpDirtyHtml>()
                .AddTransient<IServiceWeltService, ServiceWeltService>()
                .AddTransient<IServiceWeltFacade, ServiceWeltFacade>()
                .AddTransient<IHeatPumpDataRepository, HeatPumpDataRepository>()
                .AddTransient<IUnitService, UnitService>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IValueParser, ValueParser>()
                .AddTransient<IWebsiteParser, WebsiteParser>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddTransient<IHeatPumpStatisticsCalculator, HeatPumpStatisticsCalculator>()
                .AddHostedService<ApplicationLifetimeService>()
                .Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(45))
                // Executes at every 30th minute.
                .AddCronJob<CollectHeatPumpDataJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    c.CronExpression = @"*/30 * * * *";
                })
                // Executes every day at 00:00 local time
                .AddCronJob<HeatPumpStatisticsCalculatorJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    c.CronExpression = @"0 0 * * *";
                }).AddHostedService<PeriodLimitFixHostedServiceJob>();
            services.AddHttpClientForServiceWelt(new Uri(Configuration["ServiceWeltUrl"]));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(o => { });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backup/Restore API V1");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
