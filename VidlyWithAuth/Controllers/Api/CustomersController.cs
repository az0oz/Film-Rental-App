using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Http;
using VidlyWithAuth.Dtos;
using VidlyWithAuth.Models;

namespace VidlyWithAuth.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null) 
        {
            var customersQuery = _context.Customers
                .Include(cus => cus.MembershipType);
               
            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(customer => customer.Name.Contains(query));
            }

            var customerDtos = customersQuery
                 .ToList()
                 .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer (int id)
        {
            var customer = _context.Customers.SingleOrDefault(cus => cus.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // check if customer fields are valid and not empty 
            // otherwise throw bad request 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //if all fields are fields then add customers data
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/{id}
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(cus => cus.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //we invoke the two argument Mapper.Map method
            // which track changes in (source, destination)
            Mapper.Map(customerDto, customerInDb);

            //update if customer is found and fields are valid
            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;

            _context.SaveChanges();
        }

        // DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(cus => cus.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //if customer is found using supplied id 
            //then remove customer from DB

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }


    }
}
