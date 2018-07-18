using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Controllers
{
    [Route("/api/stateinitialisers")]
    public class StateInitialiserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IStateInitialiserRepository stateRepository;
        private readonly IUnitOfWork unitOfWork;
        public StateInitialiserController(IMapper mapper, IStateInitialiserRepository stateRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.stateRepository = stateRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateInitialiser(int id)   
        {
            var stateInitialiser = await stateRepository.GetStateInitialiser(id);

            if (stateInitialiser == null)
                return NotFound();

            var result = mapper.Map<StateInitialiser, StateInitialiserResource>(stateInitialiser);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetStateInitialisers()   
        {
            var stateInitialiser = await stateRepository.GetStateInitialisers();

            if (stateInitialiser == null)
                return NotFound();

            var result = mapper.Map<ICollection<StateInitialiser>, ICollection<StateInitialiserResource>>(stateInitialiser);

            return Ok(result);
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateNewState([FromBody] SaveVehicleResource vehicleResource)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);
        // }
    }
}