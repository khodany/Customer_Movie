using AutoMapper;
using Customer_Movie.Dtos;
using Customer_Movie.Models;
using Customer_Movie.Repos;
using CustomerMoviess.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Customer_Movie.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private Icustomer _customer;
        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _customer = new CustomerRepository(context);
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers(string query = null)
        {
            var customersQuery = _customer.GetCustomer();

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = (List<Customer>)customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return customerDtos;
        }

        // GET /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _customer.GetCustomerByIdUpdate(id);

            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST /api/customers
        [HttpPost]
        public int CreateCustomer(CustomerDto customerDto)
        {
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            return _customer.SaveCustomer(customer);
        }

        // PUT /api/customers/1
        [HttpPut]
        public int UpdateCustomer(int id, CustomerDto customerDto)
        { 
            var customerInDb = _customer.GetCustomerByIdUpdate(id);
            Mapper.Map(customerDto, customerInDb);
           return _context.SaveChanges();

        }

        // DELETE /api/customers/1
        [HttpDelete]
        public int DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            _context.Customers.Remove(customerInDb);
           return _context.SaveChanges();
        }
    }
}
