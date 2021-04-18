using System.Threading.Tasks;

namespace StiebelEltronApiServer.Repositories
{
    public interface IUnitOfWork
    {
        public IHeatPumpDataRepository HeatPumpDataRepository {get; set;}
        public Task<int> SaveChanges();
    }
}