using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.DataAccess
{
    public class ActorMovieRelationship
    {
        [Key]
        public int RowId { get; set; }
        //todo: add the combined key here
        public int ActorId { get; set; }
        public int MovieId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
