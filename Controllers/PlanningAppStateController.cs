using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core;
using System.Globalization;
using vega.Extensions.DateTime;
using Microsoft.Extensions.Options;
using vega.Core.Models.Settings;
using vega.Core.Utils;
using System.Linq;

namespace vega.Controllers
{
    [Route("/api/planningappstate")]
    public class PlanningAppStateController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPlanningAppStateRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPlanningAppRepository planningAppRepository;

        public PlanningAppStateController(IMapper mapper,
                                     IPlanningAppStateRepository repository,
                                     IPlanningAppRepository planningAppRepository,
                                     IUnitOfWork unitOfWork)
        {
            this.planningAppRepository = planningAppRepository;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;

        }

        [HttpGet("{id}")]
        public async Task<PlanningAppStateFullResource> GetPlanningAppState(int id)
        {
            var planningAppState = await repository.GetPlanningAppState(id);

            var planningApp = await planningAppRepository.GetPlanningApp(planningAppState.PlanningAppId);

            var planningAppStateResource = Mapper.Map<PlanningAppState, PlanningAppStateFullResource>(planningAppState);

            //populate the custom fields with values set in 'customStateValue'
            foreach( var customStateValueResource in planningAppStateResource.StateRules) {
                var stateRule = planningAppState.getRule(customStateValueResource.Id);
                if(stateRule != null) //Can be null is new rule added after creation of planning app
                    customStateValueResource.Value = planningAppState.getRule(customStateValueResource.Id).StrValue;
            }

            DateTime minDueDate = planningAppState.SetMinDueByDate(planningApp);
            planningAppStateResource.MinDueByDate = minDueDate.SettingDateFormat();
            planningAppStateResource.DueByDateEditable = minDueDate > SystemDate.Instance.date;
            
            return planningAppStateResource;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlanningAppState(int id, [FromBody] UpdatePlanningAppStateResource planningAppStateResource)
        {
            var planningAppState = await repository.GetPlanningAppState(id);
            var planningApp = await planningAppRepository.GetPlanningApp(planningAppState.PlanningAppId);
            var dueByDate = planningAppStateResource.DueByDate.ParseInputDate();
            
            if(dueByDate != planningAppState.DueByDate) {
                planningAppState.UpdateCustomDueByDate(dueByDate);
                planningApp.generateDueByDates();  //Regenerate due by dates
            }

            //Set any fields in the PlanningApp table that have been set in the Rule List
            foreach (var customStateValueResource in planningAppStateResource.StateRules) 
                planningAppState.getRule(customStateValueResource.Id).StrValue = customStateValueResource.Value;

            //Store custom fields in planning state
            planningApp.UpdateKeyFields(planningAppStateResource.StateRules);

            planningAppState.Notes = planningAppStateResource.Notes;
            repository.Update(planningAppState);
            await unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}