using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheComputerShop.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TheComputerShop.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(
                new AppUser
                {
                    Id = "bc2be71f-a9ab-4c8f-a264-5bdfb27cabb0",
                    Name = "admin@test.com",
                    UserName = "admin@test.com",
                    NormalizedUserName = "ADMIN@test.com",
                    Email = "admin@test.com",
                    NormalizedEmail = "ADMIN@test.com",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAECNF403oNaq42f9TusQOn65NiV3ZSGmMG9Gg4NOa/8Mwcjt49ThnH1ihQZ3X/o/+5g==",//@Mipassword123
                    LockoutEnabled = false,
                    AccessFailedCount = 0

                });
        }
    }
}
