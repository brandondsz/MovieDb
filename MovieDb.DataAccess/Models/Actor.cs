using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MovieDb.DataAccess
{
    public class Actor
    {
        public Actor()
        {
            ActorMovieRelationships = new List<ActorMovieRelationship>();
        }

        [Key]
        public int RowId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }


        public virtual ICollection<ActorMovieRelationship> ActorMovieRelationships { get; set; }
    }
}
