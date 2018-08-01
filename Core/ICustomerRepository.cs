using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(int id, bool includeRelated = true);
        Task<QueryResult<Customer>> GetCustomers(CustomerQuery queryObj);
    }
}