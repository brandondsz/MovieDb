using MovieDb.DataAccess;
using MovieDb.Repository.Implementation;
using MovieDb.Repository.Interfaces;
using MovieDb.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MovieDb.Controllers
{
    public class ProducerController : Controller
    {
         private IProducerRepository _producerRepository = null;
        public ProducerController()
        {
            this._producerRepository = new ProducerRepository();
        }

        public JsonResult GetProducers(string text)
        {
            var producers = _producerRepository.GetAllQueryable().Select(x => new ProducerViewModel
            {
                Name = x.Name,
                RowId = x.RowId
            });

            if (!string.IsNullOrEmpty(text))
            {
                producers = producers.Where(x => x.Name.Contains(text));
            }

            return Json(producers, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateEdit(int? producerId)
        {
            ViewBag.ProducerId = producerId;
            return View();
        }

        public ActionResult DisplayProducer(int producerId)
        {
            var producer = _producerRepository.GetById(producerId);
            if (producer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProducerViewModel()
            {
                Bio = producer.Bio,
                DateOfBirth = producer.DateOfBirth,
                Name = producer.Name,
                Sex = producer.Sex,
                RowId = producer.RowId
            };

            return View(viewModel);
        }


        [HttpGet]
        public ActionResult CreateEditProducer(int? producerId)
        {
            if (producerId.HasValue)
            {
                var producer = _producerRepository.GetById(producerId);
                if (producer == null)
                {
                    return HttpNotFound();
                }
                var viewModel = new ProducerViewModel()
                {
                    Bio = producer.Bio,
                    DateOfBirth = producer.DateOfBirth,
                    Name = producer.Name,
                    Sex = producer.Sex,
                    RowId = producer.RowId
                };
                return PartialView("_CreateEditProducer", viewModel);
            }
            else
            {
                return PartialView("_CreateEditProducer");
            }
        }

        [HttpPost]
        public ActionResult CreateEditProducer(ProducerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var producer = new Producer()
                {
                    Bio = viewModel.Bio,
                    DateOfBirth = viewModel.DateOfBirth,
                    Name = viewModel.Name,
                    Sex = viewModel.Sex
                };
                _producerRepository.Insert(producer);

                _producerRepository.Save();

                return Json(new { success = true, producerId = producer.RowId });
            }
            else
            {
                return PartialView("_CreateEditProducer", viewModel);
            }
        }

    }
}