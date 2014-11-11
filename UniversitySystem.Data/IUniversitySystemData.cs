namespace UniversitySystem.Data
{
    using UniversitySystem.Data.Repositories;
    using UniversitySystem.Models;

    public interface IUniversitySystemData
    {
        IRepository<Course> Courses { get; }

        IRepository<Department> Departments { get; }

        IRepository<Homework> Homework { get; }

        IRepository<Lecture> Lectures { get; }

        IRepository<Lecturer> Lecturers { get; }

        void SaveChanges();
    }
}
