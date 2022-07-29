using Customer_Movie.Models;
using Customer_Movie.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;


namespace CustomerMoviess.Data
{
    public class MovieRepository : IMovie
    {
        private ApplicationDbContext _context;
        private IMovie _movie;//private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int SaveMovie(Movie movie)
        {
            _context.Movies.Add(movie);

            return _context.SaveChanges();
        }
        public int updateMovie(Movie movie)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
            movieInDb.Name = movie.Name;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.NumberInStock = movie.NumberInStock;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            return _context.SaveChanges();
        }
        public Movie GetMovieById(int Id)
        {
         return _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
        }
        public Movie GetMovieByIdupdate(int Id)
        {
            return _context.Movies.SingleOrDefault(m => m.Id == Id);

        }
        public List<Genre> GetAllGenre()
        {
            return _context.Genres.ToList();
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
