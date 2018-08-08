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

namespace vega.Controllers
{
    [Route("/api/statestatus")]
    public class StateStatusController: Controller
    {
        private readonly IMapper mapper;
        private readonly IStateStatusRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public StateStatusController(IMapper mapper, IStateStatusRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;

        }


        [HttpGet]
        public async Task<IEnumerable<StateStatusResource>> GetStatuses()
        {
            var statuses = await repository.GetStateStatusList(true);

            return Mapper.Map<List<StateStatus>, List<StateStatusResource>>(statuses);
        }
    }
}