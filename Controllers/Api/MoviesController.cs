using AutoMapper;
using Customer_Movie.Dtos;
using Customer_Movie.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Web.Mvc;
using Customer_Movie.Repos;
using CustomerMoviess.Data;

namespace Customer_Movie.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private IMovie _movie;
        ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _movie = new MovieRepository(context);
            _context = new ApplicationDbContext();
        }

        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }


        public MovieDto GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            return Mapper.Map<Movie, MovieDto>(movie);
        }

        //[HttpPost]
        //[Authorize(Roles = RoleName.CanManageMovies)]
        public int CreateMovie(MovieDto movieDto)
        {
         

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return _context.SaveChanges();
        }

        public int UpdateMovie(int id, MovieDto movieDto)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            Mapper.Map(movieDto, movieInDb);
            return _context.SaveChanges();
        }

        public int DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return _context.SaveChanges();
        }
    }
}
