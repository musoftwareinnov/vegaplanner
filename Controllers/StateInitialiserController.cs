using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;
using vega.Core.Models.States;
using vega.Controllers.Resources.StateInitialser;
using System;

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

        [HttpPost]
        public async Task<IActionResult> CreateStateInitialiser([FromBody] StateInitialiserSaveResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stateInitialiser = mapper.Map<StateInitialiserSaveResource, StateInitialiser>(resource);

            stateInitialiser.LastUpdate = DateTime.Now;
            stateRepository.Add(stateInitialiser);

            await unitOfWork.CompleteAsync();

            stateInitialiser = await stateRepository.GetStateInitialiser(stateInitialiser.Id);

            var result = mapper.Map<StateInitialiser, StateInitialiserResource>(stateInitialiser);

            return Ok(result);
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
        public async Task<QueryResultResource<StateInitialiserResource>> GetPlanningApps(StateInitialiserQueryResource filterResource)
        {
            var filter = mapper.Map<StateInitialiserQueryResource, StateInitialiserQuery>(filterResource);
            
            var queryResult = await stateRepository.GetStateInitialisers(filter);

            return mapper.Map<QueryResult<StateInitialiser>, QueryResultResource<StateInitialiserResource>>(queryResult);
        }
    }
}