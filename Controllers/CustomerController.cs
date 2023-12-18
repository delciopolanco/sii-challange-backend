﻿using Microsoft.AspNetCore.Mvc;
using POS_service_customers.Services;
using POS_service_customers.Models;
using Microsoft.EntityFrameworkCore;
using POS_service_customers.DTOs;
using AutoMapper;
using System;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS_service_customers.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(CustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Customer>>> Get(int page = 1, int pageSize = 10)
        {
            var query = _customerRepository.Queryable<CustomerIdDTO>().OrderBy(c => c.Name).Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(await query.ToListAsync());
        }

        [HttpPost()]
        public async Task<ActionResult<Customer>> Post(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);

            await _customerRepository.Insert(customer);

            return Created("", customerDTO);
        }

        [HttpPut()]
        public async Task<ActionResult> Put(CustomerIdDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);

            if (string.IsNullOrEmpty(customer.Id.ToString())) return new BadRequestResult();

            await _customerRepository.Update(customer);

            return Ok();
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete(int Id)
        {

            if (string.IsNullOrEmpty(Id.ToString())) return new BadRequestResult();

            var customer = _customerRepository.Queryable().Where(e => e.Id == Id).FirstOrDefault();

            if (customer == null || customer.Active == false) return new BadRequestResult();

            await _customerRepository.Queryable().Where(e => e.Id == Id).ExecuteUpdateAsync(e => e.SetProperty(s => s.Active, s => false));

            return Ok();
        }

    }
}
