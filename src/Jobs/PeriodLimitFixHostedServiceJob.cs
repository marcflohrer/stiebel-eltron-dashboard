using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StiebelEltronDashboard.Models;
using StiebelEltronDashboard.Repositories;
using StiebelEltronDashboard.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Jobs
{
    public class PeriodLimitFixHostedServiceJob : IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger logger;

        public PeriodLimitFixHostedServiceJob(IServiceScopeFactory serviceScopeFactory, ILogger logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = this.serviceScopeFactory.CreateScope();
            using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var periodsToFix = await unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecordsWithoutPeriodStartEndAsync(4000);
            do
            {
                this.logger.Information("Fixing period start & period end");
                foreach (var p in periodsToFix)
                {
                    var periodKind = Enum.Parse<PeriodKind>(p.PeriodKind);
                    p.PeriodStart = PeriodDateProvider.GetPeriodStart(p.First.Year, periodKind, p.PeriodNumber);
                    p.PeriodEnd = PeriodDateProvider.GetPeriodEnd(p.First.Year, periodKind, p.PeriodNumber);
                }
                await unitOfWork.SaveChanges();
                periodsToFix = await unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecordsWithoutPeriodStartEndAsync(4000);
            } while (periodsToFix.Count > 0);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.Information("Done fixing period start & end");
            return Task.CompletedTask;
        }
    }
}

