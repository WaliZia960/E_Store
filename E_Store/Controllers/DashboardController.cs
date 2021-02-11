using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (MySession.User != null)
            {
                return View();
            }
            return RedirectToAction("../Login/Login");
        }
    }
}