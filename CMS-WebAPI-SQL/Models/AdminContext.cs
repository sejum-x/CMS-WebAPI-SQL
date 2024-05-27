using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI_SQL.Models
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
    }
}
