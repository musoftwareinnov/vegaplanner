using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;

namespace vega.Mapping.MappingProfiles
{
    public class PlanningAppMapping : Profile
    {
        public PlanningAppMapping()
        {
            CreateMap<PlanningApp, PlanningAppResource>()
                .ForMember(psr => psr.CurrentStateStatus,
                    opt => opt.MapFrom(ps => ps.PlanningAppStates.Where(p => p.CurrentState == true).SingleOrDefault().StateStatus.Name))
                .ForMember(psr => psr.CurrentState,
                    opt => opt.MapFrom(ps => ps.PlanningAppStates.Where(p => p.CurrentState == true).SingleOrDefault().state.Name))
                .ForMember(psr => psr.NextState,
                    opt => opt.MapFrom(ps => ps.PlanningAppStates.Where(p => p.CurrentState == false).Take(1).SingleOrDefault().state.Name));
            
            CreateMap<CreatePlanningAppResource, PlanningApp>();
        }
    }
}