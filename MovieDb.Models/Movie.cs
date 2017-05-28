using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace MovieDb.Models
{
    public class Movie
    {
        //[Key]
        public int RowId { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }


        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}
