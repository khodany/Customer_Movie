using Customer_Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer_Movie.Repos
{
    public interface IMovie
    {
        int SaveMovie(Movie movie);
        int updateMovie(Movie movie);
        Movie GetMovieById(int Id);
        List<Genre> GetAllGenre();
        Movie GetMovieByIdupdate(int Id);
    }
}
