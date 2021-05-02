using System.Threading.Tasks;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Repositories {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _applicationDbContext;
        public IHeatPumpDataRepository HeatPumpDataRepository { get; set; }
        public IHeatPumpStatisticsPerPeriodRepository HeatPumpStatisticsPerPeriodRepository { get; set; }

        public UnitOfWork (ApplicationDbContext applicationDbContext) {            
            HeatPumpDataRepository = new HeatPumpDataRepository (applicationDbContext);
            HeatPumpStatisticsPerPeriodRepository = new HeatPumpStatisticsPerPeriodRepository(applicationDbContext);
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> SaveChanges () {
            return await _applicationDbContext.SaveChangesAsync ();
        }
    }
}