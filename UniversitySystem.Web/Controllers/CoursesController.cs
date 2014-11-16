namespace UniversitySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

    [Authorize(Roles = "Teacher, Admin")]
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
                var currentLecturer = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == User.Identity.Name);
                course.Lecturers.Add(currentLecturer);
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

        [AllowAnonymous]
        [Authorize(Roles = "Student, Teacher, Admin")]
        public ActionResult Details(int id)
        {
            var course = this.data.Courses.Find(id);
            return View(course);
        }

        public ActionResult Delete(int id)
        {
            var course = this.data.Courses.Find(id);
            this.data.Courses.Delete(course);
            this.data.SaveChanges();

            var message = string.Format("Course {0} deleted!", course.Title);
            TempData["message"] = message;
            TempData["type"] = NotificationType.Warning;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var course = this.data.Courses.Find(id);
            return View(course);
        }

        [HttpPost]
        [ValidateInput(enableValidation: false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                this.data.Courses.Update(course);
                this.data.SaveChanges();

                var message = string.Format("Course \"{0}\" updated!", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Success;

                return RedirectToAction("Details", "Courses", new { id = course.Id });
            }
            else
            {
                var message = string.Format("You are trying to input some invalid data or you haven't filled all fields!", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Error;

                return RedirectToAction("Edit", "Courses", new { id = course.Id });
            }
        }

        [AllowAnonymous]
        [Authorize(Roles = "Student, Teacher, Admin")]
        public ActionResult All()
        {
            var courses = this.data.Courses.All();
            return View(courses);
        }

        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        public ActionResult JoinStudent(int id)
        {
            var student = this.data.Students.All().FirstOrDefault(s => s.User.UserName == User.Identity.Name);
            var course = this.data.Courses.Find(id);
            if (!course.Students.Contains(student))
            {
                course.Students.Add(student);
                this.data.SaveChanges();

                var message = string.Format("You have joined course \"{0}\" as a student", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Success;
            }
            else
            {
                var message = string.Format("You already participate in course \"{0}\"", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Error;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult JoinTeacher(int id)
        {
            var lecturer = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == User.Identity.Name);
            var course = this.data.Courses.Find(id);
            if (!course.Lecturers.Contains(lecturer))
            {
                course.Lecturers.Add(lecturer);
                this.data.SaveChanges();

                var message = string.Format("You have joined course \"{0}\" as a lecturer", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Success;
            }
            else
            {
                var message = string.Format("You already participate in course \"{0}\"", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Error;
            }
            
            return RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        public ActionResult LeaveStudent(int id)
        {
            var student = this.data.Students.All().FirstOrDefault(s => s.User.UserName == User.Identity.Name);
            var course = this.data.Courses.Find(id);
            if (course.Students.Contains(student))
            {
                course.Students.Remove(student);
                this.data.SaveChanges();

                var message = string.Format("You have left course \"{0}\"!", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Warning;
            }
            else
            {
                var message = string.Format("You don't participate in this course anyway!", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Error;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LeaveTeacher(int id)
        {
            var lecturer = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == User.Identity.Name);
            var course = this.data.Courses.Find(id);
            if (course.Lecturers.Contains(lecturer))
            {
                course.Lecturers.Remove(lecturer);
                this.data.SaveChanges();

                var message = string.Format("You are no longer a lecturer of course \"{0}\"!", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Warning;
            }
            else
            {
                var message = string.Format("You don't participate in this course anyway!", course.Title);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Error;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}