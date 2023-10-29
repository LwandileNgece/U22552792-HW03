using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using U22552792_HW03.Models;

namespace U22552792_HW03.Controllers
{
    public class typesController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: types
        public async Task<ActionResult> Index()
        {
            return View(await db.types.ToListAsync());
        }

        // GET: types/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type type = await db.types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DataViewModel dataViewModel)
        {
            if (ModelState.IsValid)
            {
                db.types.Add(dataViewModel.NewType);
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain","Home");
            }

            return View(dataViewModel);
        }

        // GET: types/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type type = await db.types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DataViewModel dataViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dataViewModel.NewType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain","Home");
            }
            return View(dataViewModel);
        }

        // GET: types/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type type = await db.types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            type type = await db.types.FindAsync(id);
            db.types.Remove(type);
            await db.SaveChangesAsync();
            return RedirectToAction("Maintain", "Home");
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
