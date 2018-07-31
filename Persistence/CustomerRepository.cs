using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;

namespace vega.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly VegaDbContext vegaDbContext;
        public CustomerRepository(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;

        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await vegaDbContext.Customers
                            .Where(c => c.Id == id)
                                .Include(t => t.planningApps)
                                    .ThenInclude(b => b.CurrentPlanningStatus) 
                                .Include(t => t.planningApps)
                                    .ThenInclude(t => t.PlanningAppStates)
                                        .ThenInclude(s => s.state) 
                                .Include(t => t.planningApps)
                                    .ThenInclude(t => t.PlanningAppStates) 
                                        .ThenInclude(a => a.StateStatus)

                                .SingleOrDefaultAsync();

            //Important to keep order of states as they can be added and removed - 
            //EF Core cant do Include(t => t.States.Orderby)
            var orderedStates = customer.planningApps.OrderBy(o => o.Id);

            return customer;
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await vegaDbContext.Customers
                                .OrderBy(c => c.LastName)
                                .ToListAsync();

        }
    }
}