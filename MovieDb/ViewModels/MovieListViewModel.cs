using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDb.ViewModels
{
    public class MovieListViewModel
    {
        public int MovieId { get; set; }
        public string Poster { get; set; }
        public string Title  { get; set; }
        public string Plot { get; set; }
        public int ReleaseYear { get; set; }

        public string Actors { get; set; }
        public string Producer { get; set; }
    }
}