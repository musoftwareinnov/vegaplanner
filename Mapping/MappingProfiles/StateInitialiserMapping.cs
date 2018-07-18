using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Mapping.MappingProfiles
{
    public class StateInitialiserMapping : Profile
    {
        public StateInitialiserMapping() {
                CreateMap<StateInitialiser, StateInitialiserResource>();
                CreateMap<SaveStateInitialiserStateResource, StateInitialiserState>();
        }
    }
}