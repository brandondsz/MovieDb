using MovieDb.DataAccess;
using MovieDb.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.Repository.Implementation
{
    public class MovieRepository :  BaseRepository<Movie>,IMovieRepository
    {

        //public void RemoveActorFromMovie(int actorId, int movieId)
        //{
        //    DbConfiguration.
        //}
    }
}
