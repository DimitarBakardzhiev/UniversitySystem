namespace UniversitySystem.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditLecturerModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$")]
        public string LastName { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}