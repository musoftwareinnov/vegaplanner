using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;

namespace vega.Mapping.MappingProfiles
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<ContactResource, Contact>();
            CreateMap<Contact, ContactResource>(); 
        }

    }
}