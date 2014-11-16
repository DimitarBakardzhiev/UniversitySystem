using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Data;
using UniversitySystem.Web.Models;

namespace UniversitySystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private IUniversitySystemData data;

        public StudentController()
            : this(new UniversitySystemData())
        {
        }

        public StudentController(IUniversitySystemData data)
        {
            this.data = data;
        }

        public ActionResult Profile()
        {
            var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.data.Students.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

            return View(currentUserProfile);
        }

        public ActionResult Courses()
        {
            var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.data.Students.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);
            var courses = currentUserProfile.Courses.AsQueryable();

            return View(courses);
        }

        public ActionResult EditProfile()
        {
            var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.data.Students.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);
            var departments = this.data.Departments.All();
            var model = new EditStudentsProfileViewModel(currentUserProfile, departments);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(enableValidation: false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileModel student)
        {
            if (ModelState.IsValid)
            {
                var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
                var currentUserProfile = this.data.Students.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

                currentUserProfile.FirstName = student.FirstName;
                currentUserProfile.LastName = student.LastName;
                currentUserProfile.DepartmentId = student.DepartmentId;
                this.data.SaveChanges();

                TempData["message"] = "Your profile has been updated!";
                TempData["type"] = NotificationType.Success;

                return RedirectToAction("Profile", "Student");
            }
            else
            {
                TempData["message"] = "You are trying to input some invalid data!";
                TempData["type"] = NotificationType.Error;

                return RedirectToAction("EditProfile", "Student");
            }
        }
    }
}