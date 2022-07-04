using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCoiner.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            var last = db.GlobalDashboardLists.OrderByDescending(p => p.Id)
                       .FirstOrDefault();

            return View(last);
        }

        public ActionResult Terms()
        {

            return View();
        }

        public ActionResult Privacy()
        {

            return View();
        }
    }
}