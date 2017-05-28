using MovieDb.DataAccess;
using MovieDb.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.Repository.Implementation
{
    public class ActorMovieRelationshipRepository : BaseRepository<ActorMovieRelationship>, IActorMovieRelationshipRepository
    {
    }
}
