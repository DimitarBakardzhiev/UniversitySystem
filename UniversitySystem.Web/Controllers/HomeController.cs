namespace UniversitySystem.Web.Controllers
{
    using System.Web.Mvc;

    using UniversitySystem.Data;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            // var seed = new SeedData();
            // seed.CreateDepartments();
            // seed.CreateUserRoles();

            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}