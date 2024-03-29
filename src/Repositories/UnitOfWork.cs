using System;
using System.Threading.Tasks;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext applicationDbContext;
        public IHeatPumpDataRepository HeatPumpDataRepository { get; set; }
        public IHeatPumpStatisticsPerPeriodRepository HeatPumpStatisticsPerPeriodRepository { get; set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            HeatPumpDataRepository = new HeatPumpDataRepository(applicationDbContext);
            HeatPumpStatisticsPerPeriodRepository = new HeatPumpStatisticsPerPeriodRepository(applicationDbContext);
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<int> SaveChanges()
        {
            return await applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            applicationDbContext.Dispose();
        }
    }
}