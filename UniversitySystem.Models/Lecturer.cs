using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversitySystem.Models
{
    public class Lecturer
    {
        public Lecturer()
        {
            this.Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
