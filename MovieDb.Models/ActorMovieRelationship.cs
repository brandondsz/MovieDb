using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.Models
{
    public class ActorMovieRelationship
    {
        [Key]
        public int RowId { get; set; }
        public int ActorId { get; set; }
        public int MovieId { get; set; }
    }
}
