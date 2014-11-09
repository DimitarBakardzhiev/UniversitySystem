using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UniversitySystem.Models
{
    public class Homework
    {
        [Key, ForeignKey("Lecture")]
        public int Id { get; set; }

        public string Description { get; set; }

        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}
