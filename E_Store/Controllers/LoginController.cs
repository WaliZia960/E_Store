using E_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.Controllers
{
    public class LoginController : Controller
    {
        BlogEntities1 blogEntities = new BlogEntities1();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public bool Validate_Login(string Email, string Password)
        {
            var check = blogEntities.Logins.Where(e => e.Email == Email && e.Password == Password).FirstOrDefault();
            if (check != null)
            {
                MySession.User = check;
                return true;
            }
            return false;
        }

        public ActionResult Logout()
        {
            MySession.User = null;
            return RedirectToAction("../Login/Login");
        }
    }
}