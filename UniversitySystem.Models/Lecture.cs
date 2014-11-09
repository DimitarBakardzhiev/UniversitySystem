using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversitySystem.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int HomeworkId { get; set; }

        public virtual Homework Homework { get; set; }
    }
}
