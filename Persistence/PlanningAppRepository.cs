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

    		DateTime today = DateTime.Now;

            foreach(var state in stateInitialiser.States) {
                
                PlanningAppState newState = new PlanningAppState();

                //Refactor to Business Logic
                newState.state = state;
                newState.DueByDate = today.AddDays(state.CompletionTime); //exclude weekends
                
                var status = stateStatusSettings.STATE_ON_TIME;
                var initialStatus = vegaDbContext.StateStatus.Where(s => s.Name == stateStatusSettings.STATE_ON_TIME).SingleOrDefault();
                newState.StateStatus = initialStatus;

                planningApp.PlanningAppStates.Add(newState);
                //planningApp.PlanningAppStates.Add()
            }
            vegaDbContext.Add(planningApp);   
        }
    }
}