using MovieDb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDb.Helpers
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString DisplayActors(this HtmlHelper htmlHelper,List<UserItem> actors)
        {
            return MvcHtmlString.Create(CustomDataHelper.HtmlActorDisplay(actors));
        }

        public static MvcHtmlString DisplayProducer(this HtmlHelper htmlHelper, UserItem producer)
        {
            return MvcHtmlString.Create(CustomDataHelper.HtmlProducerDisplay(producer));
        }
    }
}