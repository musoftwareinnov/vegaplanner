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
    [Route("/api/planningapps")]
    public class PlanningAppController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPlanningAppRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStateInitialiserRepository stateInitialiserRepository;

        public IStateStatusRepository statusListRepository { get; }
        public DateSettings dateSettings { get; set; }

        public DateTime CurrentDate { get; set; }
        private readonly int NextState = 1;
        private readonly int PrevState = 2;

        public PlanningAppController(IMapper mapper, 
                                     IPlanningAppRepository repository, 
                                     IUnitOfWork unitOfWork,
                                     IStateStatusRepository statusListRepository,
                                     IStateInitialiserRepository stateInitialiserRepository,
                                     IOptionsSnapshot<DateSettings> options)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
            this.statusListRepository = statusListRepository;
            this.stateInitialiserRepository = stateInitialiserRepository;
            dateSettings = options.Value;

            //TODO: refactor out of controller
            if(options.Value.CurrentDateOverride == "")
                CurrentDate = DateTime.Now;
            else {
                CurrentDate = options.Value.CurrentDateOverride.ParseInputDate();
            }

            //Store as singleton date for entire controller
            CurrentDateSingleton.setDate(CurrentDate);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanningApp([FromBody] CreatePlanningAppResource planningResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var planningApp = mapper.Map<CreatePlanningAppResource, PlanningApp>(planningResource);

            var stateInitialiser = await stateInitialiserRepository.GetStateInitialiser(planningApp.StateInitialiserId, includeDeleted: false);
            
            PlanningAppResource result = null;

            if(stateInitialiser.States.Count > 0)
            {
                repository.Add(planningApp, stateInitialiser);

                await unitOfWork.CompleteAsync();

                planningApp = await repository.GetPlanningApp(planningApp.Id, includeRelated: true);
                result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);
                result.BusinessDate = CurrentDate.SettingDateFormat();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanningApp(int id)
        {
                
            var planningApp = await repository.GetPlanningApp(id, includeRelated: true);

            if (planningApp == null)
                return NotFound();          

            var result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);
            result.BusinessDate = CurrentDate.SettingDateFormat();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlanningAppState(int id, [FromBody] UpdatePlanningAppResource planningResource)
        {
            DateTime currentDate = DateTime.Now;
            var stateStatusList = await statusListRepository.GetStateStatusList(); //List of possible statuses

            var planningApp = await repository.GetPlanningApp(id, includeRelated: true);

            if (planningApp == null)
                return NotFound();
  
            //Add possibility of override for testing
            // if(planningResource.CurrentStateCompletionDate != null)
            //     currentDate = planningResource.CurrentStateCompletionDate.ParseInputDate();

            if(planningResource.method == NextState) {
                planningApp.NextState(stateStatusList);
                //Inject Logger to say what changed state by which user
            }
            else if (planningResource.method == PrevState) 
                planningApp.PrevState(stateStatusList);
            else 
                {
                ModelState.AddModelError("Update Planning App", "Invalid Instuction Method Id: " + planningResource.method);
                    return BadRequest(ModelState);
                }     
      
            //Save to database
            repository.UpdatePlanningApp(planningApp);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);
            result.BusinessDate = CurrentDate.SettingDateFormat();

            planningApp = await repository.GetPlanningApp(id, includeRelated: true);
            return Ok(result);
        }

        [HttpGet]
        public QueryResultResource<PlanningAppSummaryResource> GetPlanningApps(PlanningAppQueryResource filterResource)
        {
            var filter = mapper.Map<PlanningAppQueryResource, PlanningAppQuery>(filterResource);
            
            var queryResult = repository.GetPlanningApps(filter);

            return mapper.Map<QueryResult<PlanningApp>, QueryResultResource<PlanningAppSummaryResource>>(queryResult);
        }
    }
}