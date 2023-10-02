using Microsoft.EntityFrameworkCore;

namespace courseApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Registry> Registries => Set<Registry>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        
    }
}