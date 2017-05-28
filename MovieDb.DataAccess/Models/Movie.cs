using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDb.DataAccess
{
    public class Movie
    {
        public Movie()
        {
            ActorMovieRelationships = new List<ActorMovieRelationship>();
        }
        [Key]
        public int RowId { get; set; }
    
        public string Plot { get; set; }
        public int ReleaseYear { get; set; }
        public string Poster { get; set; }
        public string Title { get; set; }

        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public virtual Producer Producer { get; set; }

        public virtual ICollection<ActorMovieRelationship> ActorMovieRelationships { get;set; }

        
    }
}
