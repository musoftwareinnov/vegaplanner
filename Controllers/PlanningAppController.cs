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

            //vehicle.LastUpdate = DateTime.Now;

            var planningApp = mapper.Map<CreatePlanningAppResource, PlanningApp>(planningResource);

            repository.Add(planningApp);

            await unitOfWork.CompleteAsync();

            //PlanningApp planningApp = await repository.GetPlanningApp();

            // var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok();
        }
    }
}