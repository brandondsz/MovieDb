using MovieDb.DataAccess;
using MovieDb.Repository.Implementation;
using MovieDb.Repository.Interfaces;
using MovieDb.ViewModels;
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

    }
}