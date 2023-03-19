using StiebelEltronDashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Repositories;
public interface IHeatPumpDataRepository
{
    public void InsertHeatPumpData(HeatPumpDatum heatPumpDatum);
    HeatPumpDatum[] GetLastYear();
    Task<List<HeatPumpDatum>> AllAsync();
}