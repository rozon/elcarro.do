using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ElCarro.Web.Models;
using System.Linq;
using System;

namespace ElCarro.Web.Controllers
{
    [Authorize(Roles = Constants.CompanyRole)]
    public class VehiclePartsController : CustomController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var companyId = GetCompanyId();
            var result = await db.VehiclePart
                .Where(v => v.Store.Company.Id == companyId)
                .ToListAsync();

            return View(result);
        }
        private int GetCompanyId()
        {
            var userId = GetUserId();
            return db.Company.Single(c => c.Admin.Id == userId).Id;
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = GetUserId();
            var companyId = GetCompanyId();
            var vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.Store.Company.Id == companyId);
            if (vehiclePart == null)
                return HttpNotFound();

            return View(vehiclePart);
        }

        public ActionResult Create() => View(NewViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,Photo,Model,Make,Store,Year")] CreateVehiclePart vehiclePart)
        {
            if (vehiclePart.Photo == null)
                ModelState.AddModelError(nameof(CreateVehiclePart.Photo), "The Photo is required");

            if (ModelState.IsValid)
            {
                string fullPath = SavePhoto(vehiclePart.Photo);

                var userId = GetUserId();
                db.VehiclePart.Add(new VehiclePart()
                {
                    Name = vehiclePart.Name,
                    Store = db.Stores.Single(m => m.StoreID == vehiclePart.Store),
                    Description = vehiclePart.Description,
                    Photo = fullPath,
                    Model = db.Models.Single(m => m.Id == vehiclePart.Model),
                    LastView = DateTime.Now,
                    Year = vehiclePart.Year
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            FillViewModelDropDowns(vehiclePart);
            return View(vehiclePart);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = GetUserId();
            var companyId = GetCompanyId();
            var vehiclePart = await db.VehiclePart
                .SingleOrDefaultAsync(m => m.Id == id.Value
                    && m.Store.Company.Id == companyId);

            if (vehiclePart == null)
                return HttpNotFound();

            var model = NewViewModel(vehiclePart);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Photo,Model,Make,Store,Year")] CreateVehiclePart vehiclePart)
        {
            if (ModelState.IsValid)
            {
                var companyId = GetCompanyId();
                var actual = await db.VehiclePart.SingleOrDefaultAsync(
                    m => m.Id == vehiclePart.Id && m.Store.Company.Id == companyId);

                if (actual == null)
                    return HttpNotFound();

                var newPhoto = vehiclePart.Photo != null;
                var oldPhotoPath = actual.Photo;
                if (newPhoto)
                    actual.Photo = SavePhoto(vehiclePart.Photo);
                actual.Description = vehiclePart.Description;
                actual.Name = vehiclePart.Name;
                actual.Year = vehiclePart.Year;
                actual.Store = db.Stores.Single(m => m.StoreID == vehiclePart.Store);
                actual.Description = vehiclePart.Description;
                actual.Model = db.Models.Single(m => m.Id == vehiclePart.Model);

                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        await db.SaveChangesAsync();
                        if (newPhoto)
                            System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(oldPhotoPath));
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        //If some exception is thrown then delete the temporal photo added
                        trans.Rollback();
                        if (newPhoto)
                            System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(actual.Photo));
                        throw e;
                    }
                }

                return RedirectToAction("Details", new { id = actual.Id });
            }
            FillViewModelDropDowns(vehiclePart);
            return View(vehiclePart);
        }

        public async Task<ActionResult> PublicDetails(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(m => m.Id == id.Value);
            if (vehiclePart == null)
                return HttpNotFound();

            return View(vehiclePart);
        }

        public JsonResult All()
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
                Photo = Url.Content(v.Photo)
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var companyId = GetCompanyId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.Store.Company.Id == companyId);

            if (vehiclePart == null)
                return HttpNotFound();

            return View(vehiclePart);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var companyId = GetCompanyId();
            var vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.Store.Company.Id == companyId);

            if (vehiclePart == null)
                return HttpNotFound();

            db.VehiclePart.Remove(vehiclePart);
            await db.SaveChangesAsync();
            System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(vehiclePart.Photo));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private CreateVehiclePart NewViewModel(VehiclePart vehiclePart = null)
        {
            var companyId = GetCompanyId();
            var model = CreateVehiclePart.Factory(db.Makes.AsEnumerable(),
                db.Stores.Where(m => m.Company.Id == companyId).AsEnumerable(), db.Models, vehiclePart);
            return model;
        }

        private void FillViewModelDropDowns(CreateVehiclePart vehiclePart)
        {
            var companyId = GetCompanyId();
            vehiclePart.FillDropDowns(db.Makes.AsEnumerable(),
                db.Stores.Where(m => m.Company.Id == companyId).AsEnumerable(), db.Models);
        }
    }
}