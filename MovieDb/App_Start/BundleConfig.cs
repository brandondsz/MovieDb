using System.Web;
using System.Web.Optimization;

namespace MovieDb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js")
                        
                        );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));
            /*===============Kendo================*/
            bundles.Add(new ScriptBundle("~/bundles/kendoJs")
                .Include("~/Scripts/kendo/2017.1.223/kendo.all.min.js")
                .Include("~/Scripts/kendo/2017.1.223/kendo.aspnetmvc.min.js")
                .Include("~/Scripts/kendo/2017.1.223/jszip.min.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/errorhandler").Include(
                "~/Scripts/errorhandler.js"));

            bundles.Add(new ScriptBundle("~/bundles/movieDb").Include(
                        "~/Scripts/movieDb/common.js"));

            /*===============Kendo CSS================*/
            bundles.Add(new StyleBundle("~/bundles/kendoCss")
                .Include("~/Content/kendo/kendo.common.min.css")
                .Include("~/Content/kendo/kendo.default.min.css")
                .Include("~/Content/kendo/kendo.custom.css")
                //.Include("~/Content/kendo/kendo.mobile.all.min.css")
                );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            /*===============MovieDb App Modules================*/
            bundles.Add(new ScriptBundle("~/bundles/movieDbMovie").Include(
                "~/Scripts/movieDb/movie.js"));


            bundles.Add(new ScriptBundle("~/bundles/movieDbActor").Include(
                "~/Scripts/movieDb/actor.js"));


            bundles.Add(new ScriptBundle("~/bundles/movieDbProducer").Include(
                "~/Scripts/movieDb/producer.js"));
        }
    }
}
