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
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Search(SearchViewModel viewModel)
        {
            var all = db.VehiclePart.AsQueryable();
            if (viewModel.Year != 0)
                all = all
                    .Where(m => m.Year == viewModel.Year);

            if (viewModel.Make != 0)
                all = all
                    .Where(m => m.Model.Make.Id == viewModel.Make);

            if (viewModel.Model != 0)
                all = all
                    .Where(m => m.Model.Id == viewModel.Model);

            if (!string.IsNullOrWhiteSpace(viewModel.NameOrDescription))
                all = all
                    .Where(m => m.Name.Contains(viewModel.NameOrDescription) || m.Description.Contains(viewModel.NameOrDescription));

            viewModel.FillDropDowns(db.Makes, db.Models);

            viewModel.Results = all.ToList().Skip((viewModel.ActualPage - 1) * SearchViewModel.ResultsPerPages).Take(SearchViewModel.ResultsPerPages);
            viewModel.Total = all.Count();
            viewModel.ConfigPaginator();

            return View(viewModel);
        }

        public ActionResult Store()
        {
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}