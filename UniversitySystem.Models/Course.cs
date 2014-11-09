using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySystem.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Lecturers = new HashSet<Lecturer>();
            this.Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}
