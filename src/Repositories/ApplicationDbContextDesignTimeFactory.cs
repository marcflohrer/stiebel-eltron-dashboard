using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json.Linq;
using StiebelEltronDashboard.Models;
using System;
using System.IO;
using System.Net.NetworkInformation;

namespace StiebelEltronDashboard.Repositories
{


    /// <summary>
    /// Q: Why this class is needed? 
    /// A: You get that error because to generate migrations you need either:
    ///    A DbContext with a default constructor(that is, a parameterless constructor)
    ///    Being able to get the DbContext from ApplicationServices(that is, Dependency Injection)
    ///    A design time factory that returns a properly configured DbContext.
    ///    https://github.com/dotnet-architecture/eShopOnContainers/issues/1080
    /// </summary>
    public class ApplicationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public IConfiguration Configuration { get; private set; }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build config
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.dev.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>()
                .Build();

            var connection = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new Exception("Database connection is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connection);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

