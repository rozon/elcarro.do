using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ElCarro.Web.Models;
using System;
using System.Threading.Tasks;

namespace ElCarro.Web.Controllers
{
    [Authorize(Roles = Constants.CompanyRole)]
    public class StoresController : CustomController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stores
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Manage");
        }

        // GET: Stores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            if (string.IsNullOrEmpty(store.Logo))
                store.Logo = "static/user.png";

            store.PhoneNumber = FormatPhoneNumber(store.PhoneNumber);

            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoreView store)
        {
            if (ModelState.IsValid)
            {
                Store temp = new Store(store);
                temp.Logo = SavePhoto(store.Logo);
                temp.PhoneNumber = OnlyNumbers(temp.PhoneNumber);
                string userId = GetUserId();
                temp.Company = await db.Company.SingleAsync(c => c.Admin.Id.Equals(userId));
                temp = db.Stores.Add(temp);

                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Manage");
            }

            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Store temp = await db.Stores.FindAsync(id);
            StoreView store = new StoreView(temp);
            store.PhoneNumber = FormatPhoneNumber(store.PhoneNumber);

            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StoreView model)
        {
            if (ModelState.IsValid)
            {
                Store temp = await db.Stores.FindAsync(model.StoreID);
                temp.Name = model.Name;
                temp.PhoneNumber = OnlyNumbers(model.PhoneNumber);
                temp.Email = model.Email;
                temp.latitude = model.lat_long.lat;
                temp.longitude = model.lat_long.lng;

                string actualPhotoPath = temp.Logo;
                if (null != model.Logo)
                {
                    temp.Logo = SavePhoto(model.Logo);
                    DeletePhoto(actualPhotoPath);
                }

                db.Entry(temp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Stores", new { id = temp.StoreID });
            }
            return View(model);
        }

        // GET: Stores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            string urlImage = store.Logo;
            db.Stores.Remove(store);
            await db.SaveChangesAsync();
            DeletePhoto(urlImage);
            return RedirectToAction("Index", "Manage");
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
