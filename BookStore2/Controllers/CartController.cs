﻿using BookStore2.Models;
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
        BookContext db = new BookContext();

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
        public RedirectToRouteResult AddToCart(int bookId, string returnUrl)
        {
            Book book = db.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                GetCart().AddItem(book, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int bookId, string returnUrl)
        {
            Book book = db.Books.FirstOrDefault(b => b.Id == bookId);

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
            lineCollection = GetCart().SendMail(login, db);

            if (lineCollection != null)
            {
                foreach (var line in lineCollection)
                {
                    Book b = db.Books.Find(line.Book.Id);
                    if (b == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        b.QuantityInStorage -= line.Quantity;
                        db.Entry(b).State = EntityState.Modified;
                        db.SaveChanges();
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