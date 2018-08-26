using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (BookContext db = new BookContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (BookContext db = new BookContext())
                    {
                        db.Users.Add(new User { Login = model.Login, Pass = model.Pass });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Login == model.Login && u.Pass == model.Pass).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        HttpContext.Response.Cookies["IsAdmin"].Value = user.IsAdmin.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (BookContext db = new BookContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Pass == model.Pass);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    //HttpContext.Response.Cookies["Login"].Value = user.Login;
                    //HttpContext.Response.Cookies["Pass"].Value = user.Pass;
                    HttpContext.Response.Cookies["IsAdmin"].Value = user.IsAdmin.ToString();


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            //HttpContext.Response.Cookies["Login"].Value = null;
            //HttpContext.Response.Cookies["Pass"].Value = null;
            HttpContext.Response.Cookies["IsAdmin"].Expires = DateTime.Now.AddMinutes(-1);
            return RedirectToAction("Index", "Home");
        }
    }
}