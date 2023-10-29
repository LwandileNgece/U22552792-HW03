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
    public class booksController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: books
        public async Task<ActionResult> Index()
        {
            var books = db.books.Include(b => b.author).Include(b => b.type);
            return View(await books.ToListAsync());
        }

        // GET: books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = await db.books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.authorId = new SelectList(db.authors, "authorId", "name");
            ViewBag.typeId = new SelectList(db.types, "typeId", "name");
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DataViewModel dataViewModel)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(dataViewModel.NewBook);
                await db.SaveChangesAsync();
                return RedirectToAction("Home","Home");
            }

            ViewBag.authorId = new SelectList(db.authors, "authorId", "name", dataViewModel.NewBook.authorId);
            ViewBag.typeId = new SelectList(db.types, "typeId", "name", dataViewModel.NewBook.typeId);
            return View(dataViewModel);
        }

        // GET: books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = await db.books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.authorId = new SelectList(db.authors, "authorId", "name", book.authorId);
            ViewBag.typeId = new SelectList(db.types, "typeId", "name", book.typeId);
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "bookId,name,pagecount,point,authorId,typeId")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.authorId = new SelectList(db.authors, "authorId", "name", book.authorId);
            ViewBag.typeId = new SelectList(db.types, "typeId", "name", book.typeId);
            return View(book);
        }

        // GET: books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = await db.books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            book book = await db.books.FindAsync(id);
            db.books.Remove(book);
            await db.SaveChangesAsync();
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
