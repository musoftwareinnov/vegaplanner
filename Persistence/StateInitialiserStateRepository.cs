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
    public class StateInitialiserStateRepository : IStateInitialiserStateRepository
    {
        private readonly VegaDbContext vegaDbContext;
        public StateInitialiserStateRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;
        }
        public void AddBeginning(StateInitialiserState stateInitialiserState ) {
            var stateInitialiser = GetStateInitialiser(stateInitialiserState.StateInitialiserId);

            stateInitialiserState.OrderId = stateInitialiser.States.Min(o => o.OrderId);
            //increment order for all states after
            stateInitialiser.States.ForEach(s => s.OrderId += 1);
            //updates sort orders
            vegaDbContext.Update(stateInitialiser);
            //insert new state
            vegaDbContext.Add(stateInitialiserState);
        }

        public void AddAfter(StateInitialiserState stateInitialiserState, int sortOrderId) {
            var stateInitialiser = GetStateInitialiser(stateInitialiserState.StateInitialiserId);

            //increment order for all states after specified sort id
            stateInitialiser.States.Where(x => x.OrderId > sortOrderId)
                                    .ToList()
                                    .ForEach(s => s.OrderId += 1);

            stateInitialiserState.OrderId = sortOrderId+1;
            //updates sort orders
            vegaDbContext.Update(stateInitialiser);
            //insert new state
            vegaDbContext.Add(stateInitialiserState);
        }
        private StateInitialiser GetStateInitialiser(int id) {
            return this.vegaDbContext.StateInitialisers
                .Where(si => si.Id == id)        
                .Include(s => s.States)
                .SingleOrDefault();
        }
    }
}