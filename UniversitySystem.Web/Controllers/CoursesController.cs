namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

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
                TempData["message"] = string.Format("Course {0} created!", course.Title);
                TempData["type"] = NotificationType.Success;
            }
            else
            {
                TempData["message"] = "Title and course description are required!";
                TempData["type"] = NotificationType.Error;
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}