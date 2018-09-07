using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Controllers.Resources.Contact;
using vega.Core.Models;

namespace vega.Mapping.MappingProfiles
{
    public class StateRuleMapping : Profile
    {
        public StateRuleMapping()
        {  
            CreateMap<StateRuleResource, StateRule>();
            CreateMap<StateRule, StateRuleResource>();
        }
    }
}