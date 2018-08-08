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

            CreateMap<Customer, CustomerResource>()
                    .ForMember(psr => psr.NameSummary,
                    opt => opt.MapFrom(ps =>  ps.FirstName   
                                            + ' ' + ps.LastName 
                                            + ", " + ps.Address1
                                            + ' ' + ps.Address2
                                            + ", " + ps.Postcode ));
                                            
            CreateMap<CustomerResource, Customer>()
                .ForMember(psr => psr.SearchCriteria,
                    opt => opt.MapFrom(ps =>  ps.FirstName   
                                            + ' ' + ps.LastName 
                                            + ' ' + ps.Address1
                                            + ' ' + ps.Address2
                                            + ' ' + ps.Postcode
                                            + ' ' + ps.EmailAddress
                                            + ' ' + ps.TelephoneHome
                                            + ' ' + ps.TelephoneMobile));

            CreateMap<Customer, CustomerSelectResource>()
                .ForMember(psr => psr.Name,
                    opt => opt.MapFrom(ps =>  ps.FirstName   
                                            + ' ' + ps.LastName 
                                            + ", " + ps.Address1
                                            + ' ' + ps.Address2
                                            + ", " + ps.Postcode ));
                                           
        }
    }
}