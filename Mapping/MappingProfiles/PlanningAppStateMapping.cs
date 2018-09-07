using AutoMapper;
using Microsoft.Extensions.Options;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core.Models.Settings;
using System;
using System.Globalization;
using vega.Extensions.DateTime;
using System.Linq;

namespace vega.Mapping.MappingProfiles
{
    public class PlanningAppStateMapping : Profile
    {
        //private readonly DateFormatSetting options;
        public PlanningAppStateMapping()
        {
            CreateMap<PlanningAppState, PlanningAppStateResource>()
                .ForMember(psr => psr.StateName,
                    opt => opt.MapFrom(ps => ps.state.Name))
                .ForMember(psr => psr.DueByDate,
                    opt => opt.MapFrom(ps => ps.DueByDate.SettingDateFormat())) //TODO Get from settings
                .ForMember(psr => psr.DateCompleted,
                    opt => opt.MapFrom(ps => ps.CompletionDate == null ? "" : ps.CompletionDate.Value.ToLocalTime().SettingDateFormat()))
                .ForMember(psr => psr.StateStatus,
                    opt => opt.MapFrom(ps => ps.DynamicStateStatus()));

            CreateMap<PlanningAppState, PlanningAppStateFullResource>()
                .ForMember(psr => psr.StateName,
                    opt => opt.MapFrom(ps => ps.state.Name))
                .ForMember(psr => psr.DueByDate,
                    opt => opt.MapFrom(ps => ps.DueByDate.SettingDateFormat())) 
                .ForMember(psr => psr.DateCompleted,
                    opt => opt.MapFrom(ps => ps.CompletionDate == null ? "" : ps.CompletionDate.Value.ToLocalTime().SettingDateFormat()))
                .ForMember(psr => psr.StateStatus,
                    opt => opt.MapFrom(ps => ps.DynamicStateStatus()))
                .ForMember(sis => sis.StateRules,
                    opt => opt.MapFrom(s => s.state.StateRules
                        .Select(sr => new StateRule {   Id = sr.StateRule.Id, 
                                                        Name = sr.StateRule.Name,
                                                        Type = sr.StateRule.Type,
                                                        isPlanningAppField = sr.StateRule.isPlanningAppField,
                                                        isMandatory = sr.StateRule.isPlanningAppField}))); 
        }
    }
}