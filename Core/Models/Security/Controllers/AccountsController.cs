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
using Microsoft.AspNetCore.Identity;
using vegaplanner.Core.Models.Security;
using vegaplanner.Core.Models.Security.Helpers;
using vegaplanner.Core.Models.Security.Resources;
using Microsoft.AspNetCore.Http;

using System.Linq;

namespace vegaplanner.Controllers
{
 [Route("api/[controller]")] 
    public class AccountsController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountsController(UserManager<AppUser> userManager, 
                                    IMapper mapper, 
                                    IUserRepository userRepository,
                                    IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = mapper.Map<AppUser>(model);

            var result = await userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            userRepository.Add(new InternalAppUser { IdentityId = userIdentity.Id, Location = model.Location });

            await unitOfWork.CompleteAsync();

            return new OkObjectResult("Account created");
        }

        [HttpGet]
        public async Task<IActionResult> Account()
        {
        // retrieve the user info
        //HttpContext.User
        var userId = httpContextAccessor.HttpContext.User.Claims.Single(c => c.Type == "id");
        var customer = await userRepository.Get(userId);
        
        return new OkObjectResult(new
        {
            Message = "This is secure API and user data!",
            customer.Identity.FirstName,
            customer.Identity.LastName,
            customer.Identity.PictureUrl,
            customer.Identity.FacebookId,
            customer.Location,
            customer.Locale,
            customer.Gender
        });
        }
    }

}