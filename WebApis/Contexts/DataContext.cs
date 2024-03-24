using Microsoft.EntityFrameworkCore;
using WebApis.Models;

namespace WebApis.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("TestDb");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<User_Role_Menu> User_Role_Menu { get; set; }

    }
}
