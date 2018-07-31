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

namespace vega.Controllers
{
    [Route("/api/customers")]
    public class CustomerController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICustomerRepository customerRepository;
        private readonly IUnitOfWork unitOfWork;
        public CustomerController(IMapper mapper, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)   
        {
            var customer = await customerRepository.GetCustomer(id);

            if (customer == null)
                return NotFound();

            var result = mapper.Map<Customer, CustomerResource>(customer);

            return Ok(result);
        }
    }
}