using System.ComponentModel.DataAnnotations;
using courseApp.Data;

namespace courseApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int? TeacherId { get; set; }
        public ICollection<Registry > Registries { get; set; } = new List<Registry>();
    }
}