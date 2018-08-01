using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;
using vega.Extensions;

namespace vega.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly VegaDbContext vegaDbContext;
        public CustomerRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;
        }

        public async Task<Customer> GetCustomer(int id, bool includeRelated = true)
        {
            if(includeRelated) {
                var customer = await vegaDbContext.Customers
                                .Include(t => t.planningApps)
                                    .ThenInclude(b => b.CurrentPlanningStatus) 
                                .Include(t => t.planningApps)
                                    .ThenInclude(t => t.PlanningAppStates)
                                        .ThenInclude(s => s.state) 
                                .Include(t => t.planningApps)
                                        .ThenInclude(t => t.PlanningAppStates) 
                                        .ThenInclude(a => a.StateStatus)
                                .SingleOrDefaultAsync(v => v.Id == id);

                //Important to keep order of states as they can be added and removed - 
                //EF Core cant do Include(t => t.States.Orderby)
                var orderedStates = customer.planningApps.OrderBy(o => o.Id);
                return customer;
            }
            else {
                return await vegaDbContext.Customers.FindAsync(id);
            }
        }
        public async Task<QueryResult<Customer>> GetCustomers(CustomerQuery queryObj)
        {
            var result = new QueryResult<Customer>();
            var resList = new List<Customer>();

            var query = vegaDbContext.Customers
                                .OrderBy(c => c.LastName)
                                .AsQueryable();

            result.TotalItems =  query.Count();
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();
            return result;
        }
    }
}