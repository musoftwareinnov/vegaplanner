using AutoMapper;
using Microsoft.Extensions.Options;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core.Models.Settings;

namespace vega.Mapping.MappingProfiles
{
    public class PlanningAppStateMapping : Profile
    {
        //private readonly DateFormatSetting options;
        public PlanningAppStateMapping()
        {
            //TODO!!!!!!! this.options = options.Value;  //Date format

            CreateMap<PlanningAppState, PlanningAppStateResource>()
                .ForMember(psr => psr.StateName,
                    opt => opt.MapFrom(ps => ps.state.Name))
                .ForMember(psr => psr.DueByDate,
                    opt => opt.MapFrom(ps => ps.DueByDate.ToString("dd-MM-yyyy"))) //TODO Get from settings
                .ForMember(psr => psr.StateStatus,
                    opt => opt.MapFrom(ps => ps.StateStatus.Name));
        }
    }
}