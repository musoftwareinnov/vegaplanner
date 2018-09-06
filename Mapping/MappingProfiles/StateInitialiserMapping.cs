using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Controllers.Resources.StateInitialser;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Mapping.MappingProfiles
{
    public class StateInitialiserMapping : Profile
    {
        public StateInitialiserMapping() {
                CreateMap<StateInitialiser, StateInitialiserResource>();
                CreateMap<StateInitialiserSaveResource, StateInitialiser>();
                CreateMap<SaveStateInitialiserStateResource, StateInitialiserState>();
                CreateMap<StateInitialiserStateResource, StateInitialiserState>();
                CreateMap<StateInitialiserState, StateInitialiserStateResource>()
                    .ForMember(sis => sis.StateRules,
                        opt => opt.MapFrom(s => s.StateRules
                            .Select(sr => new StateRule {   Id = sr.StateRule.Id, 
                                                            Name = sr.StateRule.Name,
                                                            Type = sr.StateRule.Type,
                                                            isPlanningAppField = sr.StateRule.isPlanningAppField,
                                                            isMandatory = sr.StateRule.isPlanningAppField})));

        }
    }
}