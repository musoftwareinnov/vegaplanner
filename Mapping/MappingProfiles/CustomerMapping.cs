using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Extensions.DateTime;

namespace vega.Mapping.MappingProfiles
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        { 
            CreateMap<PlanningCustomerResource, Customer>();
            CreateMap<Customer, PlanningCustomerResource>();

            CreateMap<Customer, CustomerResource>();
            CreateMap<CustomerResource, Customer>();
        }
    }
}