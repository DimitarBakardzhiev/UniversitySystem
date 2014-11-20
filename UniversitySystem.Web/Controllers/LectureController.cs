using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Models;
using UniversitySystem.Web.Models;

namespace UniversitySystem.Web.Controllers
{
    public class LectureController : BaseController
    {
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult All(int id)
        {
            var course = this.Data.Courses.Find(id);
            var lectures = course.Lectures.AsQueryable();

            return this.View(lectures);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int id)
        {
            return this.View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(enableValidation: false)]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int id, LectureInputModel model)
        {
            if (ModelState.IsValid)
            {
                var lecture = new Lecture
                {
                    Title = model.Title,
                    Description = model.Description,
                    CourseId = id
                };

                this.Data.Lectures.Add(lecture);
                this.Data.SaveChanges();

                this.TempData["message"] = string.Format("Lecture {0} added to course {1}", lecture.Title, this.Data.Courses.Find(id).Title);
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("All", "Lecture", new { id = id });
            }

            this.TempData["message"] = "Invalid data!";
            this.TempData["type"] = NotificationType.Error;

            return this.RedirectToAction("Create", "Lecture", new { id = id });
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int id)
        {
            var lecture = this.Data.Lectures.Find(id);

            return this.View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(enableValidation: false)]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int id, LectureInputModel model)
        {
            if (ModelState.IsValid)
            {
                var lecture = this.Data.Lectures.Find(id);
                lecture.Title = model.Title;
                lecture.Description = model.Description;

                this.Data.SaveChanges();
                this.TempData["message"] = string.Format("Lecture {0} updated", lecture.Title);
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "Invalid data!";
            this.TempData["type"] = NotificationType.Error;

            return this.RedirectToAction("Edit", "Lecture", new { id = id });
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int id)
        {
            var lecture = this.Data.Lectures.Find(id);
            this.Data.Lectures.Delete(lecture);
            this.Data.SaveChanges();

            this.TempData["message"] = "Lecture deleted!";
            this.TempData["type"] = NotificationType.Success;

            return this.RedirectToAction("Index", "Home");
        }
    }
}