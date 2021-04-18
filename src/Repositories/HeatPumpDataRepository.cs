using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Repositories {
    public class HeatPumpDataRepository : IHeatPumpDataRepository {
        private readonly ApplicationDbContext _applicationDbContext;
        public HeatPumpDataRepository (ApplicationDbContext applicatinDbContext) {
            _applicationDbContext = applicatinDbContext;
        }
        public void InsertHeatPumpData (HeatPumpData heatPump) {
            _applicationDbContext.Add(heatPump);
        }
    }
}