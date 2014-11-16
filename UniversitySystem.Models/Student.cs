﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySystem.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$")]
        public string LastName { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
