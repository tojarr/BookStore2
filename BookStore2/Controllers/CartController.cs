using BookStore2.Models;
using BookStore2.Models.Abstract;
using BookStore2.Serveces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore2.Controllers
{
    public class CartController : Controller
    {
        // создаем контекст данных
        //BookContext db = new BookContext();

        private IEmailSender _emailSender;
        IRepository<Book> _repo;

        public CartController(IEmailSender emailSender, IRepository<Book> repo)
        {
            _emailSender = emailSender;
            _repo = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        [Authorize]
        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            //Book book = db.Books.FirstOrDefault(b => b.BookId == bookId);
            Book book = _repo.GetById(Id);

            if (book != null)
            {
                GetCart().AddItem(book, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int bookId, string returnUrl)
        {
            Book book = _repo.GetById(bookId);

            if (book != null)
            {
                GetCart().RemoveLine(book);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult ClearCart(string returnUrl)
        {
            GetCart().Clear();

            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult Completed()
        {
            List<CartLine> lineCollection = new List<CartLine>();
            string login = User.Identity.Name;
            var db = _repo.Table;
            lineCollection = GetCart().SendMail(login, db, _emailSender);

            if (lineCollection != null)
            {
                foreach (var line in lineCollection)
                {
                    //Book b = db.Books.Find(line.Book.Id);
                    Book book = _repo.GetById(line.Book.Id);
                    if (book == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        book.QuantityInStorage -= line.Quantity;
                        //db.Entry(book).State = EntityState.Modified;
                        //db.SaveChanges();
                        _repo.Edit(book);
                    }
                }
                GetCart().Clear();
                return View();
            }
            else
            {
                GetCart().Clear();
                return View("~/Views/Cart/CompletedError.cshtml");
            }

        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}