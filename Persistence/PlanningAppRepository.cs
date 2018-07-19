using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.Models;
using vega.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using vega.Extensions;
using vega.Core.Models.States;
using Microsoft.Extensions.Options;

namespace vega.Persistence
{
    public class PlanningAppRepository : IPlanningAppRepository
    {
        private readonly VegaDbContext vegaDbContext;
        public PlanningAppRepository(VegaDbContext vegaDbContext, IOptionsSnapshot<StateStatusSettings> options)
        {
            this.vegaDbContext = vegaDbContext;
            stateStatusSettings = options.Value;
        }

        public StateStatusSettings stateStatusSettings { get; }

        public void Add(PlanningApp planningApp)
        {
            var stateInitialiser = vegaDbContext.StateInitialisers
                            .Where(s => s.Id == planningApp.StateInitialiserId)
                                .Include(t => t.States)
                                .SingleOrDefault();

            //Important to keep order of states as they can be added and removed - 
            //EF core cant do Include(t => t.States.Orderby)
            var orderedStates = stateInitialiser.States.OrderBy(o => o.OrderId); 
            var initialStatus = vegaDbContext.StateStatus.Where(s => s.Name == stateStatusSettings.STATE_ON_TIME).SingleOrDefault();

            planningApp.GeneratePlanningStates(orderedStates, initialStatus);
            vegaDbContext.Add(planningApp);   
        }

        public async Task<PlanningApp> GetPlanningApp(int id, bool includeRelated = true)
        {
            if(!includeRelated) {
                return await vegaDbContext.PlanningApps.FindAsync(id);
            }
            else {
                return await vegaDbContext.PlanningApps
                                .Where(s => s.Id == id)
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(s => s.state) 
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(a => a.StateStatus) 
                                    .SingleOrDefaultAsync();

            }
        }

        public PlanningApp UpdatePlanningAppState(int id, DateTime dateCompleted)
        {
            var planningApp = vegaDbContext.PlanningApps
                                .Where(s => s.Id == id)
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(s => s.state) 
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(a => a.StateStatus)
                                    .SingleOrDefault();

            //States are in sorted order when generated so will always be sorted by OrderId in database
            var currentState = planningApp.PlanningAppStates.Where(s => s.CurrentState == true).SingleOrDefault();

            if(currentState == null) return planningApp;
            
            int idx = planningApp.PlanningAppStates.IndexOf(currentState);
            if(idx+1 == planningApp.PlanningAppStates.Count) {
                //We have completed the application Update Where Required
            }
            else {
                planningApp.PlanningAppStates[idx+1].CurrentState = true;
            }
            
            planningApp.PlanningAppStates[idx].CompletionDate = dateCompleted;
            planningApp.PlanningAppStates[idx].CurrentState = false;
            planningApp.PlanningAppStates[idx].StateStatus = vegaDbContext.StateStatus.Where(s => s.Name == "Complete").SingleOrDefault();
            //Assign current state to planning app
            //planningApp.

            vegaDbContext.Update(planningApp);

            return planningApp;
        }
    }
}