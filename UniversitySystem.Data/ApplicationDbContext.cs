namespace UniversitySystem.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using UniversitySystem.Data.Migrations;
    using UniversitySystem.Models;

    public class UniversitySystemDbContext : IdentityDbContext<User>
    {
        public UniversitySystemDbContext()
            : base("UniversitySystemDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniversitySystemDbContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Lecture> Lectures { get; set; }

        public IDbSet<Homework> Homework { get; set; }

        public IDbSet<Lecturer> Lecturers { get; set; }

        public IDbSet<Department> Departments { get; set; }

        public static UniversitySystemDbContext Create()
        {
            return new UniversitySystemDbContext();
        }
    }
}
