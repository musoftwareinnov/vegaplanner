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

            DateTime minDueDate = planningAppState.SetMinDueByDate(planningApp);
            planningAppStateResource.MinDueByDate = minDueDate.SettingDateFormat();
            planningAppStateResource.DueByDateEditable = minDueDate > CurrentDateSingleton.setDate(DateTime.Now).getCurrentDate();
            return planningAppStateResource;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlanningAppState(int id, [FromBody] UpdatePlanningAppStateResource planningAppStateResource)
        {
            var planningAppState = await repository.GetPlanningAppState(id);
            var planningApp = await planningAppRepository.GetPlanningApp(planningAppState.PlanningAppId);
            var dueByDate = planningAppStateResource.DueByDate.ParseInputDate();

            //TODO!!!!!!! Only Update Custom Fields if submitted by them
            if(planningAppStateResource.Reset == true)
            {
                planningAppState.CustomDurationSet=false;
                planningAppState.CustomDuration=0;
                //Regenerate due by dates with custom completion date (if set)
                planningApp.generateDueByDates();                
            }
            else if(planningAppStateResource.UpdateCustomFieldsOnly == true)
            {
                //Set any fields in the PlanningApp table that have been set in the Rule List
                var planningAppFields = planningAppStateResource.StateRules.Where(r => r.isPlanningAppField == true).ToList();
                planningApp.UpdateKeyFields(planningAppFields);
            }
            else {
                planningAppState.UpdateCustomDueByDate(dueByDate);
                planningAppState.Notes = planningAppStateResource.Notes;
                //Regenerate due by dates with custom completion date (if set)
                planningApp.generateDueByDates();
            }

            repository.Update(planningAppState);
            await unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}