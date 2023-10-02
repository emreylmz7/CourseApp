using System.ComponentModel.DataAnnotations;

namespace courseApp.Data
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = false)]
        public DateTime StartingDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        
    }
}