namespace UniversitySystem.Web.Models
{
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using UniversitySystem.Models;

    public class UsersRolesViewModel
    {
        public UsersRolesViewModel(IQueryable<User> users, IQueryable<IdentityRole> roles)
        {
            this.users = users;
            this.roles = roles;
        }

        public IQueryable<User> users { get; set; }

        public IQueryable<IdentityRole> roles { get; set; }
    }
}