using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversitySystem.Models
{
    public class Department
    {
        public Department()
        {
            this.Students = new HashSet<Student>();
            this.Lecturers = new HashSet<Lecturer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
