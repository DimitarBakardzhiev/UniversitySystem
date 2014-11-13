using Microsoft.AspNet.Identity.EntityFramework;
namespace UniversitySystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using UniversitySystem.Data;
    using UniversitySystem.Web.Models;

    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        private IUniversitySystemData data;

        public RolesController()
            : this(new UniversitySystemData())
        {
        }

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
        public ActionResult Change(string username, string role, bool addRole)
        {
            var user = this.data.Users.All().FirstOrDefault(u => u.Email.ToLower().Contains(username.ToLower()));
            var userRole = this.data.Roles.All().FirstOrDefault(u => u.Name == role);

            if(user == null)
            {
                TempData["message"] = "User not found!";
                return RedirectToAction("Error", "Roles");
            }

            bool alreadyHasRole = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id) != null;
            if (alreadyHasRole && addRole == true)
            {
                TempData["message"] = "This user already has this role!";
                return RedirectToAction("Error", "Roles");
            }

            if (addRole == true)
            {
                user.Roles.Add(new IdentityUserRole() { RoleId = userRole.Id, UserId = user.Id });
                this.data.SaveChanges();
                var message = string.Format("{0} now has the role {1}!", user.Email, role);
                return View(model: message);
            }
            else
            {
                var roleToRemove = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id && r.UserId == user.Id);
                user.Roles.Remove(roleToRemove);
                this.data.SaveChanges();
                var message = string.Format("{0} has no longer the role {1}!", user.Email, role);
                return View(model: message);
            }
        }

        public ActionResult Details()
        {
            var users = this.data.Users.All();
            var roles = this.data.Roles.All();
            var model = new UsersRolesViewModel(users, roles);
            return View(model);
        }
        
        public ActionResult Error(string message)
        {
            return View(TempData["message"]);
        }
    }
}