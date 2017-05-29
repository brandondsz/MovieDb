using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MovieDb.DataAccess;
using MovieDb.Helpers;
using MovieDb.Repository.Implementation;
using MovieDb.Repository.Interfaces;
using MovieDb.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDb.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository _movieRepository = null;
        private IActorRepository _actorRepository = null;
        private IActorMovieRelationshipRepository _actorMovieRelationshipRepository = null;
        public HomeController()
        {
            this._movieRepository = new MovieRepository();
            this._actorRepository = new ActorRepository();
            this._actorMovieRelationshipRepository = new ActorMovieRelationshipRepository();
        }
        public ActionResult Index()
        {
            return RedirectToAction("MovieListing");
        }


        public ActionResult MovieListing()
        {
            return View();
        }

        public ActionResult QueryLatestMovies([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var movies = _movieRepository.GetAllQueryable().OrderByDescending(x => x.RowId).ToList();
                var viewModel = from m in movies
                                select new MovieListViewModel
                                {
                                    MovieId = m.RowId,
                                    Poster = m.Poster,
                                    Plot = m.Plot,
                                    ReleaseYear = m.ReleaseYear,
                                    Title = CustomDataHelper.HtmlMovieDisplay(new UserItem() { Name = m.Title, RowId = m.RowId }),
                                    Actors = CustomDataHelper.HtmlActorDisplay(m.ActorMovieRelationships.Select(a => new UserItem() { RowId = a.RowId, Name = a.Actor.Name }).ToList()),
                                    Producer = CustomDataHelper.HtmlProducerDisplay(new UserItem() { Name = m.Producer.Name, RowId = m.ProducerId })
                                };
                return Json(viewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult GetMovies(string text)
        {
            var movies = _movieRepository.GetAllQueryable();

            var viewModel = from m in movies
                            select new SelectListItem
                            {
                                Value = m.RowId.ToString(),
                                Text = m.Title
                            };

            if (!string.IsNullOrEmpty(text))
            {
                viewModel = viewModel.Where(p => p.Text.Contains(text));
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayMovie(int movieId)
        {
            var movie = _movieRepository.GetById(movieId);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieViewModel()
            {
                MovieId=movie.RowId,
                Plot = movie.Plot,
                ReleaseYear = movie.ReleaseYear,
                PosterPath = movie.Poster,
                MovieTitle = movie.Title,
                Actors = movie.ActorMovieRelationships.Select(x => new UserItem() { RowId = x.ActorId, Name = x.Actor.Name }).ToList(),
                Producer = new UserItem() { RowId = movie.ProducerId, Name = movie.Producer.Name }
            };
            return View(viewModel);
        }

        public ActionResult CreateEditMovie(int? movieId)
        {
            var viewModel = new MovieViewModel()
            {
                ActorIds = new List<int>(),
                ProducerIds = new List<int>()
            };

            if (movieId.HasValue)
            {
                var movie = _movieRepository.GetById(movieId);
                if (movie == null)
                {
                    return HttpNotFound();
                }

                viewModel.Plot = movie.Plot;
                viewModel.PosterPath = movie.Poster;
                viewModel.MovieTitle = movie.Title;
                viewModel.ReleaseYear = movie.ReleaseYear;
                viewModel.ActorIds = movie.ActorMovieRelationships.Select(x => x.ActorId).ToList();
                viewModel.ProducerIds = new List<int>() { movie.ProducerId };

            }
            return View(viewModel);

        }


        [HttpPost]
        public ActionResult CreateEditMovie(MovieViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    viewModel.ActorIds =  new List<int>() { };
                    viewModel.ProducerIds = new List<int>() { };
                    return View(viewModel);
                }
                Movie movie;
                if (viewModel.MovieId == 0)
                {
                    movie = new Movie();
                }
                else
                {
                    movie = _movieRepository.GetById(viewModel.MovieId);
                    if (movie == null)
                    {
                        return HttpNotFound();
                    }
                }

                movie.Plot = viewModel.Plot;
                movie.Title = viewModel.MovieTitle;
                movie.ReleaseYear = viewModel.ReleaseYear;
                movie.Poster = viewModel.PosterPath;
                movie.ProducerId = viewModel.ProducerIds.First();

                foreach (var item in movie.ActorMovieRelationships.Where(c => !viewModel.ActorIds.Contains(c.ActorId)).ToList())
                {                    
                    _actorMovieRelationshipRepository.Delete(item.RowId);
                }
                _actorMovieRelationshipRepository.Save();
                viewModel.ActorIds.ToList().ForEach(x =>
                {
                    if (movie.ActorMovieRelationships.FirstOrDefault(c => c.ActorId == x) == null)
                    {
                        movie.ActorMovieRelationships.Add(new ActorMovieRelationship() { ActorId = x, });
                    }
                });

                if (viewModel.MovieId == 0)
                {
                    _movieRepository.Insert(movie);
                }
                else
                {
                    _movieRepository.Update(movie);
                }
                _movieRepository.Save();

                return Json(new { success = true, movieId = movie.RowId }); 
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult SaveImage(IEnumerable<HttpPostedFileBase> files, string fileName)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    string path = System.IO.Path.Combine(Server.MapPath("~/Uploads/MoviePosters"), fileName);
                    // file is uploaded
                    file.SaveAs(path);

                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemoveImage(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Uploads/MoviePosters"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                         System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}