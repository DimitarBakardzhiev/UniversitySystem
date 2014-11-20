﻿namespace UniversitySystem.Web.Models
{
    using System.Collections.Generic;

    using UniversitySystem.Models;

    public class EditStudentsProfileViewModel
    {
        public Student Student { get; set; }

        public IEnumerable<Department> Departments { get; set; }

        public EditStudentsProfileViewModel(Student student, IEnumerable<Department> departments)
        {
            this.Student = student;
            this.Departments = departments;
        }
    }
}