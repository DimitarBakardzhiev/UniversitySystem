namespace UniversitySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity.EntityFramework;

    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

    [Authorize(Roles = "Admin")]
    public class RolesController : BaseController
    {
        public ActionResult Manage()
        {
            return this.View(Data.Roles.All());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Manage(string username, string role, bool addRole)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.Email.ToLower().Contains(username.ToLower()));
            var userRole = this.Data.Roles.All().FirstOrDefault(u => u.Name == role);

            if (user == null)
            {
                this.TempData["message"] = "User not found!";
                this.TempData["type"] = NotificationType.Error;
                return this.RedirectToAction("Index", "Home");
            }

            bool alreadyHasRole = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id) != null;
            if (alreadyHasRole && addRole == true)
            {
                this.TempData["message"] = "This user already has this role!";
                this.TempData["type"] = NotificationType.Error;
                return this.RedirectToAction("Index", "Home");
            }

            if (addRole == true)
            {
                user.Roles.Add(new IdentityUserRole() { RoleId = userRole.Id, UserId = user.Id });

                bool teacherProfileExists = this.Data.Lecturers.All()
                    .FirstOrDefault(s => s.UserId == user.Id) != null;
                
                if (role == "Teacher" && teacherProfileExists == false)
                {
                    var studentProfile = this.Data.Students.All().FirstOrDefault(s => s.UserId == user.Id);
                    var teacherProfile = new Lecturer() { FirstName = studentProfile.FirstName, LastName = studentProfile.LastName, UserId = user.Id };
                    this.Data.Lecturers.Add(teacherProfile);
                }

                this.Data.SaveChanges();

                var message = string.Format("{0} now has the role {1}!", user.Email, role);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Index", "Home");
            }
            else
            {
                var roleToRemove = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id && r.UserId == user.Id);
                user.Roles.Remove(roleToRemove);
                this.Data.SaveChanges();

                var message = string.Format("{0} has no longer the role {1}!", user.Email, role);
                this.TempData["message"] = message;
                this.TempData["type"] = NotificationType.Success;

                return this.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Details()
        {
            var users = this.Data.Users.All();
            var roles = this.Data.Roles.All();
            var model = new UsersRolesViewModel(users, roles);
            return this.View(model);
        }
        
        public JsonResult Users(string username)
        {
            var users = this.Data.Users.All()
                .Where(u => u.Email.ToLower().Contains(username.ToLower()))
                .Select(u => new { Username = u.Email })
                .ToList();
            return this.Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}