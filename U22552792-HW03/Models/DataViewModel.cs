using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U22552792_HW03.Models
{
    public class DataViewModel
    {
        public IEnumerable<student> Students { get; set; }
        public IEnumerable<book> Books { get; set; }
        public IEnumerable<author> Authors { get; set; }
        public IEnumerable<type> Types { get; set; }
        public IEnumerable<borrow> Borrows { get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public student NewStudent { get; set; }
        public book NewBook { get; set; }
        public author NewAuthor { get; set; }
        public type NewType { get; set; }
        public borrow NewBorrow { get; set; }
        public Report NewReport { get; set; }
        public IPagedList<student> StudentsPagedList { get; set; }
        public IPagedList<book> BooksPagedList { get; set; }
        public IPagedList<author> AuthorsPagedList { get; set; }
        public IPagedList<type> TypesPagedList { get; set; }
        public IPagedList<borrow> BorrowsPagedList { get; set; }

    }
}