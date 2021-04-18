using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public IHeatPumpDataRepository HeatPumpDataRepository {get; set;}
        
        public UnitOfWork(ApplicationDbContext applicationDbContext){
            HeatPumpDataRepository = new HeatPumpDataRepository(applicationDbContext);
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> SaveChanges()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }
    }
}