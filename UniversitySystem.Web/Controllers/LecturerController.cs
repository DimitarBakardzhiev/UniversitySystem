namespace UniversitySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
   
    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

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
            var departments = this.data.Departments.All();
            var model = new EditProfileViewModel(currentUserProfile, departments);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(enableValidation: false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileModel lecturer)
        {
            if (ModelState.IsValid)
            {
                var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
                var currentUserProfile = this.data.Lecturers.All().FirstOrDefault(l => l.User.UserName == currentUser.UserName);

                currentUserProfile.FirstName = lecturer.FirstName;
                currentUserProfile.LastName = lecturer.LastName;
                currentUserProfile.DepartmentId = lecturer.DepartmentId;
                this.data.SaveChanges();

                TempData["message"] = "Your profile has been updated!";
                TempData["type"] = NotificationType.Success;

                return RedirectToAction("Profile", "Lecturer");
            }
            else
            {
                TempData["message"] = "You are trying to input some invalid data!";
                TempData["type"] = NotificationType.Error;

                return RedirectToAction("EditProfile", "Lecturer");
            }
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