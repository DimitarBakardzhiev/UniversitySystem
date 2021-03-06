﻿namespace UniversitySystem.Data
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    using UniversitySystem.Data.Repositories;
    using UniversitySystem.Models;

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

        public IRepository<Student> Students
        {
            get
            {
                return this.GetRepository<Student>();
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

        public IRepository<IdentityUserRole> UserRoles
        {
            get
            {
                return this.GetRepository<IdentityUserRole>();
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
