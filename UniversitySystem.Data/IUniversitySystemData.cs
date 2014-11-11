namespace UniversitySystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
using UniversitySystem.Data.Repositories;
using UniversitySystem.Models;

    public interface IUniversitySystemData
    {
        IRepository<Course> Courses { get; }

        IRepository<Department> Departments { get; }

        IRepository<Homework> Homework { get; }

        IRepository<Lecture> Lectures { get; }

        IRepository<Lecturer> Lecturers { get; }

        IRepository<IdentityRole> Roles { get; }

        void SaveChanges();
    }
}
