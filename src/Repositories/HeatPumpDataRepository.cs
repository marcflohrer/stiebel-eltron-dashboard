using Microsoft.EntityFrameworkCore;
using StiebelEltronApiServer.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace StiebelEltronApiServer.Repositories {
    public class HeatPumpDataRepository : IHeatPumpDataRepository {
        private readonly ApplicationDbContext _applicationDbContext;

        public HeatPumpDataRepository(ApplicationDbContext applicationDbContext) 
            => _applicationDbContext = applicationDbContext;

        public HeatPumpDatum[] GetLastYear() 
             {
                 var oneYearAgo = DateTime.UtcNow.Subtract(TimeSpan.FromDays(366));
                 return _applicationDbContext.HeatPumpData.Where(a => a.DateCreated >= oneYearAgo).ToArray();
             }

        public async Task<HeatPumpDatum> GetMaxTotalPowerConsumption() {
            var maxTotalPowerConsumption = 0.0;
            if(_applicationDbContext.HeatPumpData == null){
                Console.WriteLine($"No db table found {nameof(HeatPumpDatum)}");
                maxTotalPowerConsumption = -2.0;
            }
            if(!_applicationDbContext.HeatPumpData.Any()){
                Console.WriteLine($"No total power consumption found in db.");
                maxTotalPowerConsumption = -1.0;
            }
            maxTotalPowerConsumption = await _applicationDbContext.HeatPumpData.Select(h => h.TotalPowerConsumption).MaxAsync();
            Console.WriteLine($"Max total power consumption {maxTotalPowerConsumption}");
            return new HeatPumpDatum{
                TotalPowerConsumption = maxTotalPowerConsumption
            };
        }

        public void InsertHeatPumpData(HeatPumpDatum heatPump) 
            => _applicationDbContext.HeatPumpData.Add(heatPump);

        public void RemoveRange(IEnumerable<HeatPumpDatum> heatPumpData) 
            => _applicationDbContext.HeatPumpData.RemoveRange(heatPumpData);
    }
}