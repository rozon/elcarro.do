using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ElCarro.Web.Models;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.IO;
using System;
using System.Configuration;

namespace ElCarro.Web.Controllers
{
    [Authorize(Roles = Constants.CompanyRole)]
    public class VehiclePartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            string userId = GetUserId();
            return View(await db.VehiclePart.Where(m => m.Company.Admin.Id == userId).ToListAsync());
        }

        private string GetUserId() => User.Identity.GetUserId();

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(m => m.Id == id && m.Company.Admin.Id == userId);
            if (vehiclePart == null)
            {
                return HttpNotFound();
            }
            return View(vehiclePart);
        }

        public ActionResult Create() => View(NewViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Description,Photo,Model")] CreateVehiclePart vehiclePart)
        {
            if (ModelState.IsValid)
            {
                string fullPath = SavePhoto(vehiclePart);

                var userId = User.Identity.GetUserId();
                db.VehiclePart.Add(new VehiclePart()
                {
                    Company = this.db.Company.Single(c => c.Admin.Id == userId),
                    Description = vehiclePart.Description,
                    Photo = fullPath,
                    Model = db.Models.Single(m => m.Id == vehiclePart.Id)
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            FillCreateVehiclePartModel(vehiclePart);
            return View(vehiclePart);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string userId = GetUserId();
            var vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(m => m.Id == id.Value && m.Company.Admin.Id == userId);

            if (vehiclePart == null)
                return HttpNotFound();

            var model = NewViewModel();
            vehiclePart.Id = vehiclePart.Id;
            vehiclePart.Description = vehiclePart.Description;
            FillCreateVehiclePartModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Photo")] CreateVehiclePart vehiclePart)
        {
            if (ModelState.IsValid)
            {
                string userId = GetUserId();
                VehiclePart actual = await db.VehiclePart.SingleOrDefaultAsync(m => m.Id == vehiclePart.Id && m.Company.Admin.Id == userId);
                if (vehiclePart == null)
                {
                    return HttpNotFound();
                }

                var actualPhotoPath = actual.Photo;
                actual.Description = vehiclePart.Description;
                actual.Photo = SavePhoto(vehiclePart);
                db.Entry(actual).State = EntityState.Modified;
                await db.SaveChangesAsync();
                System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(actualPhotoPath));
                return RedirectToAction("Details", new { id = actual.Id });
            }
            FillCreateVehiclePartModel(vehiclePart);
            return View(vehiclePart);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(m => m.Id == id && m.Company.Admin.Id == userId);
            if (vehiclePart == null)
            {
                return HttpNotFound();
            }
            return View(vehiclePart);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(m => m.Id == id && m.Company.Admin.Id == userId);
            if (vehiclePart == null)
            {
                return HttpNotFound();
            }
            db.VehiclePart.Remove(vehiclePart);
            await db.SaveChangesAsync();
            System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(vehiclePart.Photo));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private CreateVehiclePart NewViewModel() =>
            new CreateVehiclePart(db.Makes.AsEnumerable());

        private void FillCreateVehiclePartModel(CreateVehiclePart vehiclePart) =>
         vehiclePart.Makes =
            CreateVehiclePart.MakesSelect(db.Makes.AsEnumerable(), db.Models.Single(m => m.Id == vehiclePart.Model).Make.Id);

        private string SavePhoto(CreateVehiclePart vehiclePart)
        {
            var path = ConfigurationManager.AppSettings["FileUplodasFolder"];
            var name = Guid.NewGuid().ToString() + Path.GetExtension(vehiclePart.Photo.FileName);
            var fullPath = "~/" + path + "/" + name;
            using (var f = new FileStream(ControllerContext.HttpContext.Server.MapPath(fullPath), FileMode.CreateNew))
            {
                vehiclePart
                    .Photo
                    .InputStream
                    .CopyTo(f);
            }
            return fullPath;
        }
    }
}
