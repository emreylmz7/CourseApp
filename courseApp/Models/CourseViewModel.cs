using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using courseApp.Data;

namespace courseApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course Title Area is Required.Please Fill it")]
        [StringLength(50)]
        [Display(Name = "Course Title")]

        public string? Title { get; set; }
        public int TeacherId { get; set; }
        public ICollection<Registry > Registries { get; set; } = new List<Registry>();
    }
}