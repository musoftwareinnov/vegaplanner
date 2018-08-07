using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core;
using vega.Core.Models.States;
using vega.Persistence;
using vega.Controllers.Resources.StateInitialser;

namespace vega.Controllers
{
    [Route("/api/stateinitialiserstates")]
    public class StateInitialiserStateController : Controller
    {
        public StateInitialiserStateController(IMapper mapper, IStateInitialiserStateRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            UnitOfWork = unitOfWork;
        }

        public IMapper mapper { get; }
        public IStateInitialiserStateRepository repository { get; }
        public IUnitOfWork UnitOfWork { get; }

        [HttpPost]
        public async Task<IActionResult> SaveStateInitialiserState([FromBody] SaveStateInitialiserStateResource stateInitialiserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (stateInitialiserResource == null)
                return NotFound();

            if(stateInitialiserResource.StateInitialiserId == 0) {//Business Validation Check
                ModelState.AddModelError("InitialiserId", "InitialiserId not valid");
                return BadRequest(ModelState);
            }

            var stateInitialiserState = mapper.Map<SaveStateInitialiserStateResource, StateInitialiserState>(stateInitialiserResource);

            if(stateInitialiserResource.OrderId == 0) 
                repository.AddBeginning(stateInitialiserState);
            else 
                repository.AddAfter(stateInitialiserState, stateInitialiserResource.OrderId) ;

            await UnitOfWork.CompleteAsync();

            stateInitialiserState = await repository.GetStateInitialiserState(stateInitialiserState.Id);
            var result = mapper.Map<StateInitialiserState, StateInitialiserStateResource>(stateInitialiserState);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateInitialiserState(int id)
        {
            var stateInitialiserState = await repository.GetStateInitialiserState(id);

            if (stateInitialiserState == null)
                return NotFound();

            var result = mapper.Map<StateInitialiserState, StateInitialiserStateResource>(stateInitialiserState);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStateInitialiserState([FromBody] StateInitialiserStateResource stateInitialiserStateResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stateInitialiserState = mapper.Map<StateInitialiserStateResource, StateInitialiserState>(stateInitialiserStateResource);

            repository.Update(stateInitialiserState);

            await UnitOfWork.CompleteAsync();

            stateInitialiserState = await repository.GetStateInitialiserState(stateInitialiserState.Id);

            var result = mapper.Map<StateInitialiserState, StateInitialiserStateResource>(stateInitialiserState);

            return Ok(result);
        }
    }
}