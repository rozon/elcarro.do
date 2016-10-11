using ElCarro.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ElCarro.Web.Controllers
{
    public class ModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Model
        public JsonResult ByMake(int makeId) =>
            Json(db.Models.Where(m => m.Make.Id == makeId).Select(m => new
            {
                Id = m.Id,
                Name = m.Name,
                Selected = true
            }).ToList(), JsonRequestBehavior.AllowGet);


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
