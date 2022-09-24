using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace StiebelEltronDashboard.Services
{
    public class ApplicationLifetimeService : IHostedService
    {
        private readonly ILogger logger;

        public ApplicationLifetimeService(ILogger logger)
        {
            this.logger = logger;
        }

        private readonly IHostApplicationLifetime applicationLifetime;

        public ApplicationLifetimeService(IHostApplicationLifetime applicationLifetime, ILogger logger)
        {
            this.applicationLifetime = applicationLifetime;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // register a callback that sleeps for 30 seconds
            _ = applicationLifetime.ApplicationStopping.Register(() =>
            {
                logger.Information("SIGTERM received, waiting for 30 seconds");
                Thread.Sleep(30_000);
                logger.Information("Termination delay complete, continuing stopping process");
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
