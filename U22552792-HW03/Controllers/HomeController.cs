using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U22552792_HW03.Models;
using PagedList;
using System.IO;

namespace U22552792_HW03.Controllers
{
    public class HomeController : Controller
    {
        LibraryEntities _context = new LibraryEntities();
        ReportsEntities _reports = new ReportsEntities();
        public ActionResult Home(int? page, int? page1)
        {
            var bookslist = _context.books.ToList();
            var studentslist = _context.students.ToList();

            int pageSize = 10;
            int pageIndex = page ?? 1;
            int pageIndex1 = page1 ?? 1;

            IPagedList<student> studentsPage = _context.students.OrderByDescending(m => m.name).ToPagedList(pageIndex, pageSize);
            IPagedList<book> booksPage = _context.books.OrderByDescending(m => m.name).ToPagedList(pageIndex1, pageSize);

            var viewModel = new DataViewModel
            {
                Books = bookslist,
                Students = studentslist,
                Authors = _context.authors.ToList(),
                Types = _context.types.ToList(),
                StudentsPagedList = studentsPage,
                BooksPagedList = booksPage,
            };

            return View(viewModel);
        }
        public ActionResult Maintain(int? page, int? page1, int? page2)
        {
            var Authorslist = _context.authors.ToList();
            var Typeslist = _context.types.ToList();
            var BorrowsList = _context.borrows.ToList();

            int pageSize = 10;
            int pageIndex = page ?? 1;
            int pageIndex1 = page1 ?? 1;
            int pageIndex2 = page2 ?? 1;

            IPagedList<author> authorsPage = _context.authors.OrderByDescending(m => m.name).ToPagedList(pageIndex, pageSize);
            IPagedList<type> typesPage = _context.types.OrderByDescending(m => m.name).ToPagedList(pageIndex1, pageSize);
            IPagedList<borrow> borrowsPage = _context.borrows.OrderByDescending(m => m.takenDate).ToPagedList(pageIndex2, pageSize);

            var viewModel = new DataViewModel
            {
                Books = _context.books.ToList(),
                Students = _context.students.ToList(),
                Authors = Authorslist,
                Types = Typeslist,
                Borrows = BorrowsList,
                AuthorsPagedList = authorsPage,
                TypesPagedList = typesPage,
                BorrowsPagedList = borrowsPage
            };

            return View(viewModel);
        }
        public ActionResult Report()
        {
            var popularBooksData = _context.borrows
                .GroupBy(b => b.bookId)
                .Select(group => new PopularBooksViewModel
                {
                    BookTitle = group.FirstOrDefault().book.name,
                    BorrowCount = group.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .ToList();

            ViewBag.ChartData = popularBooksData;
            var viewModel = new DataViewModel
            {
                Reports = _reports.Reports.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Report(string FileType, string filename, string description)
        {
            // Create a new File object
            Report newFile = new Report
            {
                Name = filename,
                File_Type = FileType,
                Description = description // Use the correct parameter name here
            };

            // Add the new file to the context and save changes
            _reports.Reports.Add(newFile);
            _reports.SaveChanges();
            return RedirectToAction("Report");
        }
    }
}