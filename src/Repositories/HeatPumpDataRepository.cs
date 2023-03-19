using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Repositories
{
    public class HeatPumpDataRepository : IHeatPumpDataRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public HeatPumpDataRepository(ApplicationDbContext applicationDbContext) => this.applicationDbContext = applicationDbContext;

        public async Task<List<HeatPumpDatum>> AllAsync() => await applicationDbContext.HeatPumpData.ToListAsync();

        public async Task<List<HeatPumpDatum>> FindByDateCreated(DateTime dateCreated)
            => await applicationDbContext.HeatPumpData
                .Where(hpd => hpd.DateCreated == dateCreated)
                .ToListAsync();

        public HeatPumpDatum[] GetLastYear()
        {
            var oneYearAgo = DateTime.UtcNow.Subtract(TimeSpan.FromDays(366));
            return applicationDbContext.HeatPumpData.Where(a => a.DateCreated >= oneYearAgo).ToArray();
        }

        public void Add(HeatPumpDatum heatPump) => applicationDbContext.HeatPumpData.Add(heatPump);
        public void Remove(HeatPumpDatum heatPump) => applicationDbContext.HeatPumpData.Remove(heatPump);
        public void Update(HeatPumpDatum heatPump) => applicationDbContext.HeatPumpData.Update(heatPump);
    }
}