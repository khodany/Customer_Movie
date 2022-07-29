using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customer_Movie.Models;

namespace Customer_Movie.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}