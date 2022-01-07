using GHActions_EFMigrations.Models;

namespace GHActions_EFMigrations
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            :base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
    }
}