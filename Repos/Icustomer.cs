using Customer_Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer_Movie.Repos
{
    public interface Icustomer
    {
        int SaveCustomer(Customer customer);
        int updateCustomer(Customer customer);
        Customer GetCustomerById(int Id);
        List<MembershipType> GetMemberShipType();
        Customer GetCustomerByIdUpdate(int id);
        List<Customer> GetCustomer();
    }
}
