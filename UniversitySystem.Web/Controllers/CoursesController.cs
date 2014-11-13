namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using UniversitySystem.Data;
    using UniversitySystem.Models;

    public class CoursesController : Controller
    {
        private IUniversitySystemData data;

        public CoursesController()
            : this(new UniversitySystemData())
        {
        }

        public CoursesController(IUniversitySystemData data)
        {
            this.data = data;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(enableValidation: false)]
        public ActionResult Create(Course course)
        {
            if (this.ModelState.IsValid)
            {
                this.data.Courses.Add(course);
                this.data.SaveChanges();
            }

            return new HttpStatusCodeResult(200); // TODO
        }
    }
}