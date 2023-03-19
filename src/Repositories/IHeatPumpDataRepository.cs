using StiebelEltronDashboard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Repositories;
public interface IHeatPumpDataRepository
{
    HeatPumpDatum[] GetLastYear();
    Task<List<HeatPumpDatum>> AllAsync();
    Task<List<HeatPumpDatum>> FindByDateCreated(DateTime dateCreated);
    public void Add(HeatPumpDatum heatPumpDatum);
    void Remove(HeatPumpDatum heatPumpDatum);
    void Update(HeatPumpDatum heatPumpDatum);
}