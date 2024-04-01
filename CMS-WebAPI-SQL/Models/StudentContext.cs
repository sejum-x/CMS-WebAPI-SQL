using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI_SQL.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
