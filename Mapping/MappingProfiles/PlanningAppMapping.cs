using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;

namespace vega.Mapping.MappingProfiles
{
    public class PlanningAppMapping : Profile
    {
        public PlanningAppMapping()
        {
            CreateMap<CreatePlanningAppResource, PlanningApp>();
        }
    }
}