namespace UniversitySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;
    using UniversitySystem.Web.Models.InputModels;

    [Authorize(Roles = "Teacher")]
    public class HomeworkController : BaseController
    {
        public ActionResult Create(int lectureId)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(enableValidation: false)]
        public ActionResult Create(int lectureId, HomeworkInputModel homework)
        {
            if (ModelState.IsValid)
            {
                var newHomework = new Homework
                {
                    Description = homework.Description,
                    LectureId = lectureId
                };

                this.Data.Lectures.Find(lectureId).Homework = newHomework;
                this.Data.SaveChanges();

                this.TempData["message"] = "Homework added successfully!";
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "The description is required and must be at least 100 characters!";
            this.TempData["type"] = NotificationType.Error;

            return this.RedirectToAction("Create", "Homework", new { lectureId = lectureId });
        }

        public ActionResult Delete(int lectureId)
        {
            var homework = this.Data.Lectures.Find(lectureId).Homework;
            if (homework != null)
            {
                this.Data.Homework.Delete(homework);
                this.Data.SaveChanges();

                this.TempData["message"] = "This homework is successfully deleted!";
                this.TempData["type"] = NotificationType.Warning;

                return this.RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "Homework not found!";
            this.TempData["type"] = NotificationType.Error;

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int lectureId)
        {
            var homework = this.Data.Lectures.Find(lectureId).Homework;
            var model = new HomeworkInputModel { Id = homework.Id, Description = homework.Description };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(enableValidation: false)]
        public ActionResult Edit(int homeworkId, HomeworkInputModel model)
        {
            var homework = this.Data.Homework.Find(homeworkId);
            if (ModelState.IsValid)
            {
                homework.Description = model.Description;

                this.TempData["message"] = "Homework successfully modified!";
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "The description is required and must be at least 100 characters!";
            this.TempData["type"] = NotificationType.Error;

            return this.RedirectToAction("Edit", "Homework", new { lectureId = homework.LectureId });
        }

        [AllowAnonymous]
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult ViewHomework(int id)
        {
            var homework = this.Data.Homework.Find(id);

            return this.Content(homework.Description);
        }
    }
}