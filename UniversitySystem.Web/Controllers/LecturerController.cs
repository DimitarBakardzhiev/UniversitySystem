namespace UniversitySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
   
    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

    [Authorize(Roles = "Teacher")]
    public class LecturerController : BaseController
    {
        public ActionResult Profile()
        {
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

            return this.View(currentUserProfile);
        }

        public ActionResult EditProfile()
        {
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);
            var departments = this.Data.Departments.All();
            var model = new EditProfileViewModel(currentUserProfile, departments);

            return this.View(model);
        }

        [HttpPost]
        [ValidateInput(enableValidation: false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileModel lecturer)
        {
            if (ModelState.IsValid)
            {
                var currentUser = this.Data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
                var currentUserProfile = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

                currentUserProfile.FirstName = lecturer.FirstName;
                currentUserProfile.LastName = lecturer.LastName;
                currentUserProfile.DepartmentId = lecturer.DepartmentId;

                this.Data.SaveChanges();

                this.TempData["message"] = "Your profile has been updated!";
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Profile", "Lecturer");
            }
            else
            {
                this.TempData["message"] = "You are trying to input some invalid Data!";
                this.TempData["type"] = NotificationType.Error;

                return this.RedirectToAction("EditProfile", "Lecturer");
            }
        }

        public ActionResult Courses()
        {
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserProfile = this.Data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);
            var courses = currentUserProfile.Courses.AsQueryable();

            return this.View(courses);
        }
    }
}