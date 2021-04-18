using Microsoft.EntityFrameworkCore;
using StiebelEltronApiServer.Models;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Repositories {
    public class HeatPumpDataRepository : IHeatPumpDataRepository {
        private readonly ApplicationDbContext _applicationDbContext;
         
        public HeatPumpDataRepository (ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<HeatPumpData> GetMaxTotalPowerConsumption() {
            var maxTotalPowerConsumption = 0.0;
            if(_applicationDbContext.HeatPumpData == null){
                Console.WriteLine($"No db table found {nameof(HeatPumpData)}");
                maxTotalPowerConsumption = -2.0;
            }
            if(!_applicationDbContext.HeatPumpData.Any()){
                Console.WriteLine($"No total power consumption found in db.");
                maxTotalPowerConsumption = -1.0;
            }
            maxTotalPowerConsumption = await _applicationDbContext.HeatPumpData.Select(h => h.TotalPowerConsumption).MaxAsync();
            Console.WriteLine($"Max total power consumption {maxTotalPowerConsumption}");
            return new HeatPumpData{
                TotalPowerConsumption = maxTotalPowerConsumption
            };
        }

        public void InsertHeatPumpData (HeatPumpData heatPump) {
            _applicationDbContext.Add(heatPump);
        }
    }
}