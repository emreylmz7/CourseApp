using System.ComponentModel.DataAnnotations;

namespace courseApp.Data
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public ICollection<Registry> Registries { get; set; } = new List<Registry>();
        
    }
}