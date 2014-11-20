namespace UniversitySystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniversitySystem.Models;

    public class SeedData
    {
        private IUniversitySystemData data;

        public SeedData()
            : this(new UniversitySystemData())
        {
        }

        public SeedData(IUniversitySystemData data)
        {
            this.data = data;
        }

        public void CreateUserRoles()
        {
            IdentityRole[] roles = 
            {
                new IdentityRole() { Name = "Admin" },
                new IdentityRole() { Name = "Teacher" },
                new IdentityRole() { Name = "Student" }
            };

            foreach (var role in roles)
            {
                this.data.Roles.Add(role);
            }

            this.data.SaveChanges();
        }

        public void CreateDepartments()
        {
            Department[] departments = 
            { 
                new Department() { Name  = "Informatics" },
                new Department() { Name = "History" },
                new Department() { Name = "Architecture" },
                new Department() { Name = "Economics" }
            };

            foreach (var department in departments)
            {
                this.data.Departments.Add(department);
            }

            this.data.SaveChanges();
        }
    }
}
