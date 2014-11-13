namespace UniversitySystem.Web.Controllers
{
    using System.Web.Mvc;

    using UniversitySystem.Data;

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
            // var seed = new SeedData();
            // seed.CreateDepartments();
            // seed.CreateUserRoles();

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