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
    public class borrowsController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: borrows
        public async Task<ActionResult> Index()
        {
            var borrows = db.borrows.Include(b => b.book).Include(b => b.student);
            return View(await borrows.ToListAsync());
        }

        // GET: borrows/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            borrow borrow = await db.borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // GET: borrows/Create
        public ActionResult Create()
        {
            ViewBag.bookId = new SelectList(db.books, "bookId", "name");
            ViewBag.studentId = new SelectList(db.students, "studentId", "name");
            return View();
        }

        // POST: borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DataViewModel dataViewModel)
        {
            if (ModelState.IsValid)
            {
                db.borrows.Add(dataViewModel.NewBorrow);
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain","Home");
            }

            ViewBag.bookId = new SelectList(db.books, "bookId", "name", dataViewModel.NewBorrow.bookId);
            ViewBag.studentId = new SelectList(db.students, "studentId", "name", dataViewModel.NewBorrow.studentId);
            return View(dataViewModel);
        }

        // GET: borrows/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            borrow borrow = await db.borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookId = new SelectList(db.books, "bookId", "name", borrow.bookId);
            ViewBag.studentId = new SelectList(db.students, "studentId", "name", borrow.studentId);
            return View(borrow);
        }

        // POST: borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DataViewModel dataViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dataViewModel.NewBorrow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain","Home");
            }
            ViewBag.bookId = new SelectList(db.books, "bookId", "name", dataViewModel.NewBorrow.bookId);
            ViewBag.studentId = new SelectList(db.students, "studentId", "name", dataViewModel.NewBorrow.studentId);
            return View(dataViewModel);
        }

        // GET: borrows/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            borrow borrow = await db.borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // POST: borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            borrow borrow = await db.borrows.FindAsync(id);
            db.borrows.Remove(borrow);
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
