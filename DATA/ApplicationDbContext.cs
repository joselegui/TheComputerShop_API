using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheComputerShop.Configuration;
using TheComputerShop.Models;

namespace TheComputerShop.DATA
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<UserAspRol> UserAspRol { get; set; }

    }
}
