using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystem.Models;

namespace UniversitySystem.Web.Models
{
    public class UsersRolesViewModel
    {
        public IQueryable<User> users { get; set; }

        public IQueryable<IdentityRole> roles { get; set; }

        public UsersRolesViewModel(IQueryable<User> users, IQueryable<IdentityRole> roles)
        {
            this.users = users;
            this.roles = roles;
        }
    }
}