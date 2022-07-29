using Customer_Movie.Dtos;
using Customer_Movie.Models;
using Customer_Movie.Repos;
using CustomerMoviess.Data;
using System;
using System.Linq;
using System.Web.Http;


namespace Customer_Movie.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private Icustomer _customer;
        private IMovie _movie;
        private ApplicationDbContext _context;
        public NewRentalsController(ApplicationDbContext context)
        {
            _customer = new CustomerRepository(context);
            _movie = new MovieRepository(context);
            _context = new ApplicationDbContext();

        }

        [HttpPost]
        public int CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _customer.GetCustomerByIdUpdate(newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            return _context.SaveChanges();
        }
    }
}
