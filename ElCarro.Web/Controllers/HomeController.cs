using ElCarro.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ElCarro.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "La pagina de descripcion de tu aplicacion.";
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllVehicleParts()
        {
            return Json(db.VehiclePart.ToList().Select(v => new
            {
                v.Name,
                v.Description,
                Model = new
                {
                    v.Model.Id,
                    v.Model.Name
                },
                Make = new
                {
                    Id = v.Model.Make.Id,
                    Name = v.Model.Make.Name
                },
                v.Year,
                Photo = string.IsNullOrWhiteSpace(v.Photo) ? Url.Content("~/static/ logo_elcarro.png") : Url.Content(v.Photo)
            }).ToList());
        }

        [HttpPost]
        public JsonResult GetAllMakes()
        {
            return Json(db.Makes.ToList().Select(v => new
            {
                v.Id,
                v.Name
            }).ToList());
        }

        [HttpPost]
        public JsonResult SearchSingle(int actualPage, int resultPerPages, PartView _part)
        {
            var all = db.VehiclePart.AsQueryable();

            all = all.Where(s => s.Store.latitude != 0 || s.Store.longitude != 0);

            if (_part.Year != 0)
                all = all
                    .Where(m => m.Year == _part.Year);

            if (_part.Make != 0)
                all = all
                    .Where(m => m.Model.Make.Id == _part.Make);

            if (_part.Model != 0)
                all = all
                    .Where(m => m.Model.Id == _part.Model);

            if (!string.IsNullOrWhiteSpace(_part.NameOrDescription))
                all = all
                    .Where(m => m.Name.Contains(_part.NameOrDescription) || m.Description.Contains(_part.NameOrDescription));

            ResultSingleSearch result = new ResultSingleSearch()
            {
                Parts = all.OrderBy(m => m.Id).Skip((actualPage - 1) * resultPerPages).Take(resultPerPages).ToList().Select(v => new VehiclePartView()
                {
                    Name = v.Name,
                    Description = v.Description,
                    CompanyName = v.Store.Name,
                    lat = v.Store.latitude,
                    lng = v.Store.longitude,
                    Photo = string.IsNullOrWhiteSpace(v.Photo) ? Url.Content("~/static/logo_elcarro.png") : Url.Content(v.Photo)
                }),
                TotalParts = all.Count()
            };

            return Json(result);
        }

        /// <summary>
        /// Old, this method is the Old implementation
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        //public ActionResult Search(SearchViewModel viewModel)
        //{
        //    var all = db.VehiclePart.AsQueryable();

        //    all = all.Where(s => s.Store.latitude != 0 || s.Store.longitude != 0);

        //    foreach (var part in viewModel.parts)
        //    {
        //        if (part.Year != 0)
        //            all = all
        //                .Where(m => m.Year == part.Year);

        //        if (part.Make != 0)
        //            all = all
        //                .Where(m => m.Model.Make.Id == part.Make);

        //        if (part.Model != 0)
        //            all = all
        //                .Where(m => m.Model.Id == part.Model);

        //        if (!string.IsNullOrWhiteSpace(part.NameOrDescription))
        //            all = all
        //                .Where(m => m.Name.Contains(part.NameOrDescription) || m.Description.Contains(part.NameOrDescription));

        //        part.FillDropDowns(db.Makes, db.Models);
        //    }

        //    viewModel.Results = all.ToList().Skip((viewModel.ActualPage - 1) * SearchViewModel.ResultsPerPages).Take(SearchViewModel.ResultsPerPages);
        //    viewModel.Total = all.Count();
        //    viewModel.ConfigPaginator();

        //    return View(viewModel);
        //}

        public ActionResult Store()
        {
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Tu pagina de contacto.";

            return View();
        }
    }
}