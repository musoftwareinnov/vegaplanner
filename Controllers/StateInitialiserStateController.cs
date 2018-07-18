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

namespace vega.Controllers
{
    [Route("/api/stateinitialiserstates")]
    public class StateInitialiserStateController : Controller
    {
        static readonly int BEFORE = 1;
        static readonly int BETWEEN = 2;
        static readonly int END = 3;

        public StateInitialiserStateController(IMapper mapper, IStateInitialiserStateRepository repository, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        public IMapper Mapper { get; }
        public IStateInitialiserStateRepository Repository { get; }
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

            var stateInitialiserState = Mapper.Map<SaveStateInitialiserStateResource, StateInitialiserState>(stateInitialiserResource);

            if(stateInitialiserResource.InsertAfterStateOrderId == 0) {
                //Add to last state
                Repository.AddBeginning(stateInitialiserState);
            }
            else 
                Repository.AddAfter(stateInitialiserState, stateInitialiserResource.InsertAfterStateOrderId) ;
            
            // else {  //Default to END
            //     Repository.AddEnd(stateInitialiserState);
            // }

            await UnitOfWork.CompleteAsync();

            return Ok();
        }
    }
}