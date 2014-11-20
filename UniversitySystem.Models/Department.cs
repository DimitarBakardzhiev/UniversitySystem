namespace UniversitySystem.Models
{
    using System.Collections.Generic;

    public class Department
    {
        public Department()
        {
            this.Students = new HashSet<Student>();
            this.Lecturers = new HashSet<Lecturer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}
