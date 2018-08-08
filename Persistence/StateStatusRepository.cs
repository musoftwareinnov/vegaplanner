using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Persistence
{
    public class StateStatusRepository : IStateStatusRepository
    {
        private readonly VegaDbContext vegaDbContext;
        public StateStatusRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;

        }
  
        public async Task<List<StateStatus>> GetStateStatusList (bool includeAll = true)
        {
            if(includeAll)
                return await vegaDbContext.StateStatus.ToListAsync();
            else {
                var inProgress = vegaDbContext.StateStatus.AsQueryable();
                
                inProgress.Where(s => s.Name == "OnTime");

                return await inProgress.ToListAsync();
            }
        } 

        public StateStatus GetStateStatus(int id)
        {
            return vegaDbContext.StateStatus.Where(s => s.Id == id).SingleOrDefault();
           
        } 
    }
}