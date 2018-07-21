using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Extensions.DateTime;

namespace vega.Mapping.MappingProfiles
{
    public class PlanningAppMapping : Profile
    {
        public PlanningAppMapping()
        {
            CreateMap<PlanningApp, PlanningAppResource>()
                .ForMember(psr => psr.CurrentStateStatus,
                    opt => opt.MapFrom(ps => ps.Current().StateStatus.Name))
                .ForMember(psr => psr.CurrentState,
                    opt => opt.MapFrom(ps => ps.Current().state.Name))
                .ForMember(psr => psr.NextState,
                    opt => opt.MapFrom(ps => ps.Next().state.Name))
                .ForMember(psr => psr.PlanningStatus, 
                    opt => opt.MapFrom(ps => ps.PlanningStatus()))
                .ForMember(psr => psr.CompletionDate, 
                    opt => opt.MapFrom(ps => ps.CompletionDate().SettingDateFormat())); 
            
            CreateMap<CreatePlanningAppResource, PlanningApp>();
        }
    }
}