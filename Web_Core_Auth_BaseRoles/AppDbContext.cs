using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Web_Core_Auth_BaseRoles
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=web_core_auth_base_roles.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role[] 
            {
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "Auditor" }
            });

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User { Id = 1, Email = "User", Password = "1" },
                new User { Id = 2, Email = "Admin", Password = "1" },
                new User { Id = 3, Email = "Auditor", Password = "1" },
                new User { Id = 4, Email = "Admin Auditor", Password = "1" },
            });

            base.OnModelCreating(modelBuilder);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
