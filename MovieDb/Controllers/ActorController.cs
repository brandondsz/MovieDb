using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MovieDb.DataAccess;
using MovieDb.Repository.Implementation;
using MovieDb.Repository.Interfaces;
using MovieDb.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MovieDb.Controllers
{
    public class ActorController : Controller
    {
        private IActorRepository _actorRepository = null;
        public ActorController()
        {
            this._actorRepository = new ActorRepository();
        }

        #region GET
        public JsonResult GetActors(string text)
        {
            var actors = _actorRepository.GetAllQueryable().Select(x => new ActorViewModel
            {
                Name = x.Name,
                RowId = x.RowId
            });

            if (!string.IsNullOrEmpty(text))
            {
                actors = actors.Where(x => x.Name.Contains(text));
            }

            return Json(actors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateEdit(int? actorId)
        {
            ViewBag.ActorId = actorId;
            return View();
        }

        public ActionResult DisplayActor(int actorId)
        {
            var actor = _actorRepository.GetById(actorId);
            if (actor == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ActorViewModel()
            {
                Bio = actor.Bio,
                DateOfBirth = actor.DateOfBirth,
                Name = actor.Name,
                Sex = actor.Sex,
                RowId = actor.RowId
            };

            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateEditActor(int? actorId)
        {
            if (actorId.HasValue)
            {
                var actor = _actorRepository.GetById(actorId);
                if (actor == null)
                {
                    return HttpNotFound();
                }
                var viewModel = new ActorViewModel()
                {
                    Bio = actor.Bio,
                    DateOfBirth = actor.DateOfBirth,
                    Name = actor.Name,
                    Sex = actor.Sex,
                    RowId = actor.RowId
                };
                return PartialView("_CreateEditActor", viewModel);
            }
            else
            {
                return PartialView("_CreateEditActor");
            }
        }

        public ActionResult QueryLatestActors([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                //Hack: Order by the most recently added to a movie(this is not proper, need to use created/modified datetime instead)
                var actors = _actorRepository.GetAllQueryable().OrderByDescending(x => x.ActorMovieRelationships.Max(y => y.RowId)).ToList();
                var viewModel = from m in actors
                                select new ActorViewModel
                                {
                                    RowId = m.RowId,
                                    Name = m.Name,
                                    Bio = m.Bio,
                                    DateOfBirth = m.DateOfBirth,
                                    Sex = m.Sex
                                };

                return Json(viewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region POST
        [HttpPost]
        public ActionResult CreateEditActor(ActorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var actor = new Actor()
                {
                    Bio = viewModel.Bio,
                    DateOfBirth = viewModel.DateOfBirth,
                    Name = viewModel.Name,
                    Sex = viewModel.Sex
                };
                _actorRepository.Insert(actor);

                _actorRepository.Save();

                return Json(new { success = true, actorId = actor.RowId });
            }
            else
            {
                return PartialView("_CreateEditActor", viewModel);
            }
        }
        #endregion

    }
}