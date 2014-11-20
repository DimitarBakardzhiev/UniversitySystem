namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

    [Authorize(Roles = "Teacher, Admin")]
    public class CoursesController : BaseController
    {
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(enableValidation: false)]
        public ActionResult Create(Course course)
        {
            if (this.ModelState.IsValid)
            {
                var currentLecturer = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == User.Identity.Name);
                course.Lecturers.Add(currentLecturer);
                this.Data.Courses.Add(course);
                this.Data.SaveChanges();
                this.TempData["message"] = string.Format("Course {0} created!", course.Title);
                this.TempData["type"] = NotificationType.Success;
            }
            else
            {
                this.TempData["message"] = "Title and course description are required!";
                this.TempData["type"] = NotificationType.Error;
            }

            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [Authorize(Roles = "Student, Teacher, Admin")]
        public ActionResult Details(int id)
        {
            var course = this.Data.Courses.Find(id);
            return this.View(course);
        }

        public ActionResult Delete(int id)
        {
            var course = this.Data.Courses.Find(id);
            this.Data.Courses.Delete(course);
            this.Data.SaveChanges();

            var message = string.Format("Course {0} deleted!", course.Title);
            this.TempData["message"] = message;
            this.TempData["type"] = NotificationType.Warning;

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var course = this.Data.Courses.Find(id);
            return this.View(course);
        }

        [HttpPost]
        [ValidateInput(enableValidation: false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                this.Data.Courses.Update(course);
                this.Data.SaveChanges();

                var message = string.Format("Course \"{0}\" updated!", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Details", "Courses", new { id = course.Id });
            }
            else
            {
                var message = string.Format("You are trying to input some invalid Data or you haven't filled all fields!", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Error;

                return this.RedirectToAction("Edit", "Courses", new { id = course.Id });
            }
        }

        [AllowAnonymous]
        [Authorize(Roles = "Student, Teacher, Admin")]
        public ActionResult All()
        {
            var courses = this.Data.Courses.All();
            return this.View(courses);
        }

        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        public ActionResult JoinStudent(int id)
        {
            var student = this.Data.Students.All().FirstOrDefault(s => s.User.UserName == User.Identity.Name);
            var course = this.Data.Courses.Find(id);
            if (!course.Students.Contains(student))
            {
                course.Students.Add(student);
                this.Data.SaveChanges();

                var message = string.Format("You have joined course \"{0}\" as a student", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Success;
            }
            else
            {
                var message = string.Format("You already participate in course \"{0}\"", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Error;
            }

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult JoinTeacher(int id)
        {
            var lecturer = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == User.Identity.Name);
            var course = this.Data.Courses.Find(id);
            if (!course.Lecturers.Contains(lecturer))
            {
                course.Lecturers.Add(lecturer);
                this.Data.SaveChanges();

                var message = string.Format("You have joined course \"{0}\" as a lecturer", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Success;
            }
            else
            {
                var message = string.Format("You already participate in course \"{0}\"", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Error;
            }

            return this.RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        public ActionResult LeaveStudent(int id)
        {
            var student = this.Data.Students.All().FirstOrDefault(s => s.User.UserName == User.Identity.Name);
            var course = this.Data.Courses.Find(id);
            if (course.Students.Contains(student))
            {
                course.Students.Remove(student);
                this.Data.SaveChanges();

                var message = string.Format("You have left course \"{0}\"!", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Warning;
            }
            else
            {
                var message = string.Format("You don't participate in this course anyway!", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Error;
            }

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult LeaveTeacher(int id)
        {
            var lecturer = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == User.Identity.Name);
            var course = this.Data.Courses.Find(id);
            if (course.Lecturers.Contains(lecturer))
            {
                course.Lecturers.Remove(lecturer);
                this.Data.SaveChanges();

                var message = string.Format("You are no longer a lecturer of course \"{0}\"!", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Warning;
            }
            else
            {
                var message = string.Format("You don't participate in this course anyway!", course.Title);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Error;
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}