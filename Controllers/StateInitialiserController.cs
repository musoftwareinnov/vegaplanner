using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Core;

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

            //var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(stateInitialiser);
        }

    }
}