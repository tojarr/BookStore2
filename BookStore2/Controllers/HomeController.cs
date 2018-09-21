using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2.Models;
using BookStore2.Models.Abstract;
using PagedList;
using PagedList.Mvc;

namespace BookStore2.Controllers
{

    public class HomeController : Controller
    {

        IRepository<Book> _repo;
        public HomeController(IRepository<Book> repo)
        {
            _repo = repo;
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            IEnumerable<Book> books = _repo.Table.ToList();
            ViewBag.Books = books;

            return View(books.ToPagedList(pageNumber, pageSize));
        }


        //// создаем контекст данных
        //BookContext db = new BookContext();

        //public ActionResult Index(int? page)
        //{
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);

        //    // получаем из бд все объекты Book
        //    IEnumerable<Book> books = db.Books.ToList();

        //    //ViewBag.Books = books;

        //    // возвращаем представление
        //    return View(books.ToPagedList(pageNumber, pageSize));

        //}
    }
}