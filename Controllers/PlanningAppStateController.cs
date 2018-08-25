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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlanningAppState(int id, [FromBody] UpdatePlanningAppStateResource planningAppStateResource)
        {
            var planningAppState = await repository.GetPlanningAppState(id);

            var planningApp = await planningAppRepository.GetPlanningApp(planningAppState.PlanningAppId);
            var dueByDate = planningAppStateResource.DueByDate.ParseInputDate();

            if(planningAppStateResource.Reset == true)
            {
                planningAppState.CustomDurationSet=false;
                planningAppState.CustomDuration=0;                
            }
            else
                planningAppState.UpdateCustomDueByDate(dueByDate);

            //Regenerate due by dates with custom completion date (if set)
            planningApp.generateDueByDates();

            repository.Update(planningAppState);
            await unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}