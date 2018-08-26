using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore2.Models;

namespace BookStore2.Controllers
{

    public class HomeController : Controller
    {

        // создаем контекст данных
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book & User
            IEnumerable<Book> books = db.Books;
            IEnumerable<User> users = db.Users;

            ViewBag.Books = books;
            ViewBag.Users = users;

            // возвращаем представление
            return View();

        }

        //[Authorize]
        //public ActionResult IndexAdmin(User user)
        //{

        //    return View("~/View/Home/Index.cshtml");
        //}

        
    }
}