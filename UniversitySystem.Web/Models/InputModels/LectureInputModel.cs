namespace UniversitySystem.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class LectureInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }
    }
}