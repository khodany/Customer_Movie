using Customer_Movie.Models;
using Customer_Movie.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;


namespace CustomerMoviess.Data
{
    public class CustomerRepository : Icustomer
    {
        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public int SaveCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return _context.SaveChanges();
        }
        public int updateCustomer(Customer customer)
        {
           var customerInDb = _context.Customers.Where(x => x.Id == customer.Id).SingleOrDefault();
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            return _context.SaveChanges();
        }
        public Customer GetCustomerById(int id)
        {
           return _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
        }

        public List<MembershipType> GetMemberShipType()
        {
            return _context.MembershipTypes.ToList();
        }
        public Customer GetCustomerByIdUpdate(int id)
        {
            return _context.Customers.SingleOrDefault(c => c.Id == id);
        }

        public List<Customer> GetCustomer()
        {

            return _context.Customers.Include(c => c.MembershipType).ToList();
        }



        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
