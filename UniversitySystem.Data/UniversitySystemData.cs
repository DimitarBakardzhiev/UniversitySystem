using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversitySystem.Data.Repositories;
using UniversitySystem.Models;

namespace UniversitySystem.Data
{
    public class UniversitySystemData : IUniversitySystemData
    {
        private IUniversitySystemDbContext context;
        private IDictionary<Type, object> repositories;

        public UniversitySystemData()
            : this(new UniversitySystemDbContext())
        {
        }

        public UniversitySystemData(IUniversitySystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Course> Courses
        {
            get
            {
                return this.GetRepository<Course>();
            }
        }

        public IRepository<Department> Departments
        {
            get
            {
                return this.GetRepository<Department>();
            }
        }

        public IRepository<Homework> Homework
        {
            get
            {
                return this.GetRepository<Homework>();
            }
        }

        public IRepository<Lecture> Lectures
        {
            get
            {
                return this.GetRepository<Lecture>();
            }
        }

        public IRepository<Lecturer> Lecturers
        {
            get
            {
                return this.GetRepository<Lecturer>();
            }
        }

        public IRepository<IdentityRole> Roles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(EFRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
