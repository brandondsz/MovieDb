using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieDb.ViewModels
{
    public class ActorViewModel
    {
        public int RowId { get; set; }
        [Required(ErrorMessage = "Please enter the name of the actor")]
        public string Name { get; set; }
        public string Bio { get; set; }
        [Required(ErrorMessage = "Please select the date of birth of the actor")]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please select the sex of the actor")]
        public string Sex { get; set; }
    }
}