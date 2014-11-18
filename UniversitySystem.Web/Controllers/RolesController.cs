using Microsoft.AspNet.Identity.EntityFramework;
namespace UniversitySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using UniversitySystem.Data;
    using UniversitySystem.Models;
    using UniversitySystem.Web.Models;

    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        private IUniversitySystemData data;

        public RolesController(IUniversitySystemData data)
        {
            this.data = data;
        }
        
        public ActionResult Manage()
        {
            return View(data.Roles.All());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Manage(string username, string role, bool addRole)
        {
            var user = this.data.Users.All().FirstOrDefault(u => u.Email.ToLower().Contains(username.ToLower()));
            var userRole = this.data.Roles.All().FirstOrDefault(u => u.Name == role);

            if(user == null)
            {
                TempData["message"] = "User not found!";
                TempData["type"] = NotificationType.Error;
                return RedirectToAction("Index", "Home");
            }

            bool alreadyHasRole = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id) != null;
            if (alreadyHasRole && addRole == true)
            {
                TempData["message"] = "This user already has this role!";
                TempData["type"] = NotificationType.Error;
                return RedirectToAction("Index", "Home");
            }

            if (addRole == true)
            {
                var currentUser = this.data.Users.All().FirstOrDefault(u => u.UserName == User.Identity.Name);
                user.Roles.Add(new IdentityUserRole() { RoleId = userRole.Id, UserId = user.Id });

                bool studentProfileExists = this.data.Students.All()
                    .FirstOrDefault(s => s.FirstName == currentUser.FirstName && s.LastName == currentUser.LastName) != null;
                bool teacherProfileExists = this.data.Lecturers.All()
                    .FirstOrDefault(s => s.FirstName == currentUser.FirstName && s.LastName == currentUser.LastName) != null;
                if (role == "Student" && studentProfileExists == false)
                {
                    var studentProfile = new Student() { FirstName = user.FirstName, LastName = user.LastName, UserId = user.Id };
                    this.data.Students.Add(studentProfile);
                }
                else if (role == "Teacher" && teacherProfileExists == false)
                {
                    var teacherProfile = new Lecturer() { FirstName = user.FirstName, LastName = user.LastName, UserId = user.Id };
                    this.data.Lecturers.Add(teacherProfile);
                }

                this.data.SaveChanges();
                var message = string.Format("{0} now has the role {1}!", user.Email, role);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Success;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var roleToRemove = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id && r.UserId == user.Id);
                user.Roles.Remove(roleToRemove);
                this.data.SaveChanges();
                var message = string.Format("{0} has no longer the role {1}!", user.Email, role);
                TempData["message"] = message;
                TempData["type"] = NotificationType.Success;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Details()
        {
            var users = this.data.Users.All();
            var roles = this.data.Roles.All();
            var model = new UsersRolesViewModel(users, roles);
            return View(model);
        }
        
        public JsonResult Users(string username)
        {
            var users = this.data.Users.All()
                .Where(u => u.Email.ToLower().Contains(username.ToLower()))
                .Select(u => new { Username = u.Email })
                .ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}