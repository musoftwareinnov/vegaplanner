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
    public class StateInitialiserRepository : IStateInitialiserRepository
    {   
            private readonly VegaDbContext vegaDbContext;
        public StateInitialiserRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;

        }

        public async Task<StateInitialiser> GetStateInitialiser(int id)
        {   
            var stateInitialiser =  await vegaDbContext.StateInitialisers
                            .Where(s => s.Id == id)
                                .Include(t => t.States)
                                .SingleOrDefaultAsync();
       
            //Important to keep order of states as they can be added and removed - 
            //EF core cant do Include(t => t.States.Orderby)
            var orderedStates = stateInitialiser.States.OrderBy(o => o.OrderId); 

            stateInitialiser.States = orderedStates.ToList();

            StateInitialiserState startState = new StateInitialiserState();
            startState.CompletionTime=0;
            startState.OrderId=0;
            startState.Name="START";

            stateInitialiser.States.Insert(0, startState);

            return stateInitialiser;
        }

        public async Task<QueryResult<StateInitialiser>>  GetStateInitialisers(StateInitialiserQuery queryObj)
        {
            var result = new QueryResult<StateInitialiser>();

            var query = vegaDbContext.StateInitialisers.AsQueryable();

            result.TotalItems =  query.Count();
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;          
        }

        public void Add(StateInitialiser stateInitialiser)
        {
            vegaDbContext.Add(stateInitialiser);

        }
    }
}
   