using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystem.Data;

namespace UniversitySystem.Web.Controllers
{
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

        [HttpPost]
        public ActionResult Change(string username, string role)
        {
            var user = this.data.Users.All().FirstOrDefault(u => u.Email.ToLower().Contains(username.ToLower()));
            var userRole = this.data.Roles.All().FirstOrDefault(u => u.Name == role);

            if(user == null)
            {
                RedirectToAction("Error", new { message = "User not found!" });
            }

            bool alreadyHasRole = user.Roles.FirstOrDefault(r => r.RoleId == userRole.Id) != null;
            if (alreadyHasRole)
            {
                RedirectToAction("Error", new { message = "This user already has this role!" });
            }

            user.Roles.Add(new IdentityUserRole() { RoleId = userRole.Id, UserId = user.Id });
            this.data.SaveChanges();

            return View();
        }

        [ChildActionOnly]
        public ActionResult Error(string message)
        {
            return View(message);
        }
    }
}