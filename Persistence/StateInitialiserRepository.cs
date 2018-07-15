using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;

namespace vega.Persistence
{
    public class StateInitialiserRepository : IStateInitialiserRepository
    {
            private readonly VegaDbContext vegaDbContext;
        public StateInitialiserRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;

        }

        //   Task<StateInitialiser> GetStateInitialiser(int id);
        //   void Add(StateInitialiser StateInitialiser);

        //   void Remove(StateInitialiser StateInitialiser);

        //   Task<IEnumerable<StateInitialiser>> GetStateInitialisers();


        public async Task<StateInitialiser> GetStateInitialiser(int id)
        {
            return await vegaDbContext.StateInitialisers
                .Include(s => s.States)
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
    //     public void Add(Vehicle vehicle)
    //     {
    //         vegaDbContext.Add(vehicle);

    //     }

    //     public void Remove(Vehicle vehicle)
    //     {
    //         vegaDbContext.Remove(vehicle);

    //     }

    //     public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
    //     {
    //         var result = new QueryResult<Vehicle>();

    //         var query = vegaDbContext.Vehicle
    //             .Include(v => v.Model)
    //                 .ThenInclude(m => m.Make)
    //             .Include(v => v.Features)
    //                 .ThenInclude(vf => vf.Feature)
    //             .AsQueryable();

    //         if (queryObj.MakeId.HasValue)
    //             query = query.Where(v => v.Model.MakeId == queryObj.MakeId);

    //         if (queryObj.ModelId.HasValue)
    //             query = query.Where(v => v.ModelId == queryObj.ModelId);
                
    //         var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
    //         {
    //             ["make"] = v => v.Model.Make.Name,
    //             ["model"] = v => v.Model.Name,
    //             ["contactName"] = v => v.Contact.Name,
    //             //["id"] = v => v.Id   NOTE EF auto adds Id if not existing here!
    //         };

    //         query = query.ApplyOrdering(queryObj, columnsMap);
            
    //         result.TotalItems = await query.CountAsync();

    //         query = query.ApplyPaging(queryObj);

    //         result.Items = await query.ToListAsync();

    //         return result;     
    //     }
    // }
    // }