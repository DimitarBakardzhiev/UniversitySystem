namespace UniversitySystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using UniversitySystem.Models;

    public interface IUniversitySystemDbContext
    {
        IDbSet<Course> Courses { get; set; }

        IDbSet<Department> Departments { get; set; }

        IDbSet<Homework> Homework { get; set; }

        IDbSet<Lecturer> Lecturers { get; set; }

        IDbSet<Lecture> Lectures { get; set; }

        IDbSet<Student> Students { get; set; }

        IDbSet<IdentityUserRole> UserRoles { get; set; }

        void SaveChanges();
    }
}
