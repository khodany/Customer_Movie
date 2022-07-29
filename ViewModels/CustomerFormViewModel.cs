using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customer_Movie.Models;

namespace Customer_Movie.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}