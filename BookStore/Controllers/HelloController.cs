using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HelloName(string name, int numHellos = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumHellos = numHellos;

            return View();
        }
    }
}