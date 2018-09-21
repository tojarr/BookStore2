using BookStore2.Models;
using BookStore2.Models.Abstract;
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
        IRepository<Book> _repo;

        public BookController(IRepository<Book> repo)
        {
            _repo = repo;
        }

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
                ModelState.AddModelError("QuantityInStorage", "Некорректный ввод");
            }
            else if (model.Price < 0)
            {
                ModelState.AddModelError("QuantityInStorage", "Недопустимая цифра");
            }

            if (string.IsNullOrEmpty((model.QuantityInStorage).ToString()))
            {
                ModelState.AddModelError("Price", "Некорректная цена");
            }
            else if (model.QuantityInStorage < 0)
            {
                ModelState.AddModelError("Price", "Недопустимая цифра");
            }

            if (ModelState.IsValid)
            {
                Book book = new Book();
                if (model.ImagePath == null)
                {
                    book.ImagePath = "/Images/DefaultForBook.png";
                }
                else
                {
                    book.ImagePath = model.ImagePath;
                }
                book.Name = model.Name;
                book.Author = model.Author;
                book.Price = model.Price;
                book.QuantityInStorage = model.QuantityInStorage;
                book.Description = model.Description;

                ViewBag.Message = "Валидация пройдена";
                _repo.Save(book);

                //db.Entry(book).State = EntityState.Added;
                //db.SaveChanges();
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
            //Book book = db.Books.Find(id);
            Book book = _repo.Table.FirstOrDefault(b => b.Id == id);

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

            if (string.IsNullOrEmpty((book.QuantityInStorage).ToString()))
            {
                ModelState.AddModelError("QuantityInStorage", "Некорректный ввод");
            }
            else if (book.QuantityInStorage < 0)
            {
                ModelState.AddModelError("QuantityInStorage", "Недопустимая цифра");
            }

            if (ModelState.IsValid)
            {
                ViewBag.Message = "Валидация пройдена";
                if (book.ImagePath == null)
                {
                    book.ImagePath = "/Images/DefaultForBook.png";
                }
                //db.Entry(book).State = EntityState.Modified;
                //db.SaveChanges();
                var editModel = _repo.GetById(book.Id);
                editModel.Name = book.Name;
                editModel.Author = book.Author;
                editModel.Price = book.Price;
                editModel.QuantityInStorage = book.QuantityInStorage;
                editModel.ImagePath = book.ImagePath;
                _repo.Edit(editModel);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Запрос не прошел валидацию";
            return View(book);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book book = _repo.Table.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
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
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _repo.Table.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            _repo.Delete(book);
            //db.Books.Remove(book);
            //db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ChoiceBook(int id)
        {
            // получаем из бд все объекты Book
            var books = _repo.Table;

            ViewBag.Id = id;

            return View(books);
        }
    }
}