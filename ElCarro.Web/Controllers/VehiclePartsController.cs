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
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,Photo,Model,Store")] CreateVehiclePart vehiclePart)
        {
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
                    LastView = DateTime.Now
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Photo")] CreateVehiclePart vehiclePart)
        {
            if (ModelState.IsValid)
            {
                string userId = GetUserId();
                var companyId = GetCompanyId();
                VehiclePart actual = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == vehiclePart.Id && m.Store.Company.Id == companyId);

                if (vehiclePart == null)
                    return HttpNotFound();


                var actualPhotoPath = actual.Photo;
                actual.Description = vehiclePart.Description;
                actual.Photo = SavePhoto(vehiclePart.Photo);
                db.Entry(actual).State = EntityState.Modified;
                await db.SaveChangesAsync();
                System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(actualPhotoPath));
                return RedirectToAction("Details", new { id = actual.Id });
            }
            //FillCreateVehiclePartModel(vehiclePart);
            return View(vehiclePart);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string userId = GetUserId();
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

            string userId = GetUserId();
            var companyId = GetCompanyId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
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
            var userId = GetUserId();
            var companyId = db.Company.Single(c => c.Admin.Id == userId).Id;
            var model = CreateVehiclePart.Factory(db.Makes.AsEnumerable(),
                db.Stores.Where(m => m.Company.Id == companyId).AsEnumerable(), db.Models, vehiclePart);
            return model;
        }

        private void FillViewModelDropDowns(CreateVehiclePart vehiclePart)
        {
            var userId = GetUserId();
            var companyId = db.Company.Single(c => c.Admin.Id == userId).Id;
            vehiclePart.FillDropDowns(db.Makes.AsEnumerable(),
                db.Stores.Where(m => m.Company.Id == companyId).AsEnumerable(), db.Models);
        }
    }
}
