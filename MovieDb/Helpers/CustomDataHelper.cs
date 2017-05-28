using MovieDb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDb.Helpers
{
    public static class CustomDataHelper
    {
        public static List<SelectListItem> ItemListForSex()
        {
            return new List<SelectListItem>() {
              new SelectListItem() {
                  Text = "Male",
                  Value = "m"
              },
              new SelectListItem() {
                  Text = "Female",
                  Value = "f"
              }
          };
        }

        public static SelectList ItemListForYear()
        {
            //var items = new 
            return new SelectList(Enumerable.Range(1900, 200));
            //return new List<SelectListItem>() { Enumerable.Range(1900, 200).ToList().ForEach(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }) };
        }

        public static string HtmlActorDisplay(List<UserItem> actors)
        {
            return string.Join(",", actors.Select(x => string.Format("<a href ='/Actor/DisplayActor?actorId={0}'>{1}</a>", x.RowId, x.Name)));
        }

        public static string HtmlProducerDisplay(UserItem producer)
        {
            return string.Format("<a href ='/Producer/DisplayProducer?producerId={0}'>{1}</a>", producer.RowId, producer.Name);
        }

        public static string HtmlMovieDisplay(UserItem movie)
        {
            return string.Format("<a href ='/Home/DisplayMovie?movieId={0}'>{1}</a>", movie.RowId, movie.Name);
        }
        
    }
}