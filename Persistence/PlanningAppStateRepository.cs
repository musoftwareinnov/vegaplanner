using System.Threading.Tasks;
using vega.Core.Models;
using vega.Persistence;
using vega.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace vega.Persistence
{
    public class PlanningAppStateRepository : IPlanningAppStateRepository
    {
        private readonly VegaDbContext vegaDbContext;
        public PlanningAppStateRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;  
        }

        public async Task<PlanningAppState> GetPlanningAppState(int id)
        {

            //Refactor and call PLanningApp!!!!
            return await vegaDbContext.PlanningAppState
                                    .Where(s => s.Id == id)
                                        .Include(i => i.state)
                                    // .Where(s => s.Id == id)
                                    //     .Include(p => p.PlanningApp)
                                    //         .ThenInclude(s => s.PlanningAppStates)
                                    //             .ThenInclude(st => st.state)
                                    .SingleOrDefaultAsync();
        }

        public void Update(PlanningAppState planningAppState)
        {
            vegaDbContext.Update(planningAppState);

        }
    }
}