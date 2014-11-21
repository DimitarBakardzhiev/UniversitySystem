namespace UniversitySystem.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class HomeworkInputModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(100)]
        [AllowHtml]
        public string Description { get; set; }
    }
}