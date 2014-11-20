namespace UniversitySystem.Web.Models
{
    using System.Collections.Generic;

    using UniversitySystem.Models;

    public class EditProfileViewModel
    {
        public EditProfileViewModel(Lecturer lecturer, IEnumerable<Department> departments)
        {
            this.Lecturer = lecturer;
            this.Departments = departments;
        }

        public Lecturer Lecturer { get; set; }

        public IEnumerable<Department> Departments { get; set; }
    }
}