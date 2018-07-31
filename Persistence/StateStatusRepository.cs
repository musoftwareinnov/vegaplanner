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
  
        public async Task<List<StateStatus>> GetStateStatusList () {
            return await vegaDbContext.StateStatus.ToListAsync();
        } 
    }
}