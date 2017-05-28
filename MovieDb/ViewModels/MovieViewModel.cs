using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDb.ViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public HttpPostedFileBase Poster { get; set; }
        public string PosterPath { get; set; }

        [Required(ErrorMessage = "Please enter the title of the movie")]
        public string MovieTitle { get; set; }

        public string Plot { get; set; }

        [Required(ErrorMessage = "Please select the year of release")]
        [Display(Name = "Year of release")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Actors")]
        [Required(ErrorMessage = "Please select at least one actor")]
        public List<int> ActorIds { get; set; }
        public List<UserItem> Actors { get; set; }

        [Display(Name = "Producer")]
        [Required(ErrorMessage = "Please select a producer")]
        public List<int> ProducerIds { get; set; }
        public UserItem Producer { get; set; }
    }

    public class UserItem
    {
        public int RowId { get; set; }
        public string Name { get; set; }
    }
}