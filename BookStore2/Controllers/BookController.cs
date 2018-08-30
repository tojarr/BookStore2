using BookStore2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore2.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();

        [HttpGet]
        public ActionResult Create()
        {
            bool isAdmin = Request.Cookies["IsAdmin"]?.Value == "True" ? true : false;
            if (isAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Create(BookViewModel model)
        {
            if (string.IsNullOrEmpty((model.Price).ToString()))
            {
                ModelState.AddModelError("Price", "Некорректная цена");
            }
            else if (model.Price < 0)
            {
                ModelState.AddModelError("Price", "Недопустимая цифра");
            }

            if (ModelState.IsValid)
            {
                Book book = new Book();
                book.ImagePath = model.ImagePath;
                book.Name = model.Name;
                book.Author = model.Author;
                book.Price = model.Price;
                book.Description = model.Description;

                ViewBag.Message = "Валидация пройдена";
                db.Entry(book).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Запрос не прошел валидацию";
            return View(model);

            
        }

        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                bool isAdmin = Request.Cookies["IsAdmin"]?.Value == "True" ? true : false;
                if (isAdmin)
                {
                    return View(book);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (string.IsNullOrEmpty((book.Price).ToString()))
            {
                ModelState.AddModelError("Price", "Некорректная цена");
            }
            else if (book.Price < 0)
            {
                ModelState.AddModelError("Price", "Недопустимая цифра");
            }

            if (ModelState.IsValid)
            {
                ViewBag.Message = "Валидация пройдена";
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Запрос не прошел валидацию";
            return View(book);

           
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            bool isAdmin = Request.Cookies["IsAdmin"]?.Value == "True" ? true : false;
            if (isAdmin)
            {
                return View(b);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ChoiceBook(int id)
        {
            // получаем из бд все объекты Book & User
            var books = db.Books;

            ViewBag.Id = id;

            return View(books);
        }

        public ActionResult ThankYou()
        {

            return View();
        }
    }
}