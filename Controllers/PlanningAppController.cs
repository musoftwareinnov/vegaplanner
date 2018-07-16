using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core;
namespace vega.Controllers
{
    [Route("/api/planningapp")]
    public class PlanningAppController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPlanningAppRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public PlanningAppController(IMapper mapper, IPlanningAppRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;

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

            var result = mapper.Map<PlanningApp, PlanningAppResource>(planningApp);
            return Ok(result);
        }
    }
}