namespace UniversitySystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Lecturers = new HashSet<Lecturer>();
            this.Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public Semester Semester { get; set; }
    }
}
