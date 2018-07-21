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

namespace vega.Controllers
{
    [Route("/api/planningapp")]
    public class PlanningAppController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPlanningAppRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public IStateStatusRepository statusListRepository { get; }

        private readonly int NextState = 1;
        private readonly int PrevState = 2;
        private readonly int RollbackState = 3;
        public PlanningAppController(IMapper mapper, 
                                     IPlanningAppRepository repository, 
                                     IUnitOfWork unitOfWork,
                                     IStateStatusRepository statusListRepository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
            this.statusListRepository = statusListRepository;
        }



        [HttpPost]
        public async Task<IActionResult> CreatePlanningApp([FromBody] CreatePlanningAppResource planningResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var planningApp = mapper.Map<CreatePlanningAppResource, PlanningApp>(planningResource);
            repository.Add(planningApp);

            await unitOfWork.CompleteAsync();

            planningApp = await repository.GetPlanningApp(planningApp.Id, includeRelated: true);
            var result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanningApp(int id)
        {
            
            var planningApp = await repository.GetPlanningApp(id, includeRelated: true);

            if (planningApp == null)
                return NotFound();

            //Update statuses depending on current date.
            //planningApp.updateStatuses

            var result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);
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

            if(planningResource.CurrentStateCompletionDate != null)
                currentDate = DateTime.ParseExact(planningResource.CurrentStateCompletionDate, "dd-MM-yyyy", new CultureInfo("en-US") );

            planningApp.CurrentStateCompletionDate = currentDate;
            if(planningResource.method == NextState) {
                planningApp.NextState(stateStatusList);
                //Inject Logger to say what changed state by which user
            }
            else if (planningResource.method == PrevState) 
                planningApp.PrevState(stateStatusList);
            // else if (planningResource.method = UpdatePlanningInitialise(id)
            //     planningApp = repository.UpdatePlanningAppRollbackToState(id, stateId);
            else 
                {
                ModelState.AddModelError("Update Planning App", "Invalid Instuction Method Id: " + planningResource.method);
                    return BadRequest(ModelState);
                }

            repository.UpdatePlanningApp(planningApp);
            await unitOfWork.CompleteAsync();
         
            var result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);
            return Ok(result);
        }
    }
}