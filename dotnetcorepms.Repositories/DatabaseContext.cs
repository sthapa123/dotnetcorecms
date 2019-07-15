using dotnetcorepms.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetcorepms.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Forums> Forums { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Notes> Notes { get; set; }
    }
}
