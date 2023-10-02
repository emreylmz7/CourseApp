using System.ComponentModel.DataAnnotations;

namespace courseApp.Data
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FullName { 
            get
            {
              return this.Name + " " + this.LastName;  
            }
        }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public ICollection<Registry> Registries { get; set; } = new List<Registry>();
    }
}