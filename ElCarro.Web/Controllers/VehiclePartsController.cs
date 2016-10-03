using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ElCarro.Web.Models;
using System.Linq;
using System.Collections.Generic;

namespace ElCarro.Web.Controllers
{
    [Authorize(Roles = Constants.CompanyRole)]
    public class VehiclePartsController : CustomController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VehicleParts
        public async Task<ActionResult> Index()
        {
            string userId = GetUserId();
            List<VehiclePart> result = await db.VehiclePart.Where(
                v => v.StoreItems.Any(
                    s => s.Store.Company.Admin.Id == userId)).ToListAsync();

            return View(result);
        }

        // GET: VehicleParts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.StoreItems.Any(
                    s => s.Store.Company.Admin.Id == userId));
            if (vehiclePart == null)
            {
                return HttpNotFound();
            }
            return View(vehiclePart);
        }

        // GET: VehicleParts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Description,Photo")] CreateVehiclePart vehiclePart)
        {
            if (ModelState.IsValid)
            {
                string fullPath = SavePhoto(vehiclePart.Photo);

                var userId = GetUserId();
                db.VehiclePart.Add(new VehiclePart()
                {
                    Description = vehiclePart.Description,
                    Photo = fullPath
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehiclePart);
        }

        // GET: VehicleParts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.StoreItems.Any(
                    s => s.Store.Company.Admin.Id == userId));

            if (vehiclePart == null)
            {
                return HttpNotFound();
            }
            return View(new CreateVehiclePart()
            {
                Id = vehiclePart.Id,
                Description = vehiclePart.Description,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Photo")] CreateVehiclePart vehiclePart)
        {
            if (ModelState.IsValid)
            {
                string userId = GetUserId();
                VehiclePart actual = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == vehiclePart.Id && m.StoreItems.Any(
                    s => s.Store.Company.Admin.Id == userId));

                if (vehiclePart == null)
                {
                    return HttpNotFound();
                }

                var actualPhotoPath = actual.Photo;
                actual.Description = vehiclePart.Description;
                actual.Photo = SavePhoto(vehiclePart.Photo);
                db.Entry(actual).State = EntityState.Modified;
                await db.SaveChangesAsync();
                System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(actualPhotoPath));
                return RedirectToAction("Details", new { id = actual.Id });
            }
            return View(vehiclePart);
        }

        // GET: VehicleParts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.StoreItems.Any(
                    s => s.Store.Company.Admin.Id == userId));

            if (vehiclePart == null)
            {
                return HttpNotFound();
            }
            return View(vehiclePart);
        }

        // POST: VehicleParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = GetUserId();
            VehiclePart vehiclePart = await db.VehiclePart.SingleOrDefaultAsync(
                m => m.Id == id.Value && m.StoreItems.Any(
                    s => s.Store.Company.Admin.Id == userId));

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
    }
}
