using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Data;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class LecturerController : Controller
    {
        private IUniversitySystemData data;

        public LecturerController()
            : this(new UniversitySystemData())
        {
        }

        public LecturerController(IUniversitySystemData data)
        {
            this.data = data;
        }

        public ActionResult Profile()
        {
            var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

            return View(currentUserProfile);
        }

        public ActionResult EditProfile()
        {
            var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

            return View(currentUserProfile);
        }

        public ActionResult Courses()
        {
            var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);
            var courses = currentUserProfile.Courses.AsQueryable();

            return View(courses);
        }
    }
}