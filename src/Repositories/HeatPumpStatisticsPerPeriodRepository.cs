using StiebelEltronApiServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace StiebelEltronApiServer.Repositories
{
    public class HeatPumpStatisticsPerPeriodRepository : IHeatPumpStatisticsPerPeriodRepository {
        private readonly ApplicationDbContext _applicationDbContext;
         
        public HeatPumpStatisticsPerPeriodRepository (ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public HeatPumpDataPerPeriod FindByYearAndPeriodNumber(int year, string periodNumber) 
            => _applicationDbContext.HeatPumpDataPerPeriods.Where(hpdpp => hpdpp.Year == year 
                && hpdpp.PeriodNumber == periodNumber).FirstOrDefault();

        public void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod){
            _applicationDbContext.HeatPumpDataPerPeriods.Add(heatPumpDataPerPeriod);
        }
    }
}