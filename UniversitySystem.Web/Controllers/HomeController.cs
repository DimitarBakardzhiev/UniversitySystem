using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUniversitySystemData data;

        public HomeController()
            : this(new UniversitySystemData())
        {
        }

        public HomeController(IUniversitySystemData data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}