using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Repositories;
using StiebelEltronApiServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using StiebelEltronApiServer.Extensions;

namespace StiebelEltronApiServer
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new Exception("Database connetion is not set.");
            }
            // Add framework services.

            // DbContext pooling: AddDbContextPool enables pooling of DbContext instances. 
            // Context pooling can increase throughput in high-scale scenarios such as web servers by reusing context instances, 
            // rather than creating new instances for each request.
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddMvc();
            services.AddTransient<IXpathService, XpathService>()
                .AddTransient<IHtmlParser, HtmlParser>()
                .AddTransient<ITidyUpDirtyHtml, TidyUpDirtyHtml>()
                .AddTransient<IScrapingService, ScrapingService>()
                .AddTransient<IServiceWeltFacade, ServiceWeltFacade>()
                .AddTransient<IHeatPumpDataRepository, HeatPumpDataRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddHostedService<ApplicationLifetimeService>()
                .Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(45))
                .AddCronJob<CollectHeatPumpDataJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    c.CronExpression = @"0 * * * *";
                });

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
