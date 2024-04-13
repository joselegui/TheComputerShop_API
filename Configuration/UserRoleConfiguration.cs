using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheComputerShop.Models;

namespace TheComputerShop.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserAspRol>
    {
        public void Configure(EntityTypeBuilder<UserAspRol> builder)
        {
            builder.HasData(
                    new UserAspRol()
                    {
                        Id = "097409b0-9c63-46c5-b62c-7b47059b9370",
                        Name = "Super Administrator",
                        NormalizedName = "SUPER ADMINISTRATOR"
                    },
                    new UserAspRol()
                    {
                        Id = "84a2de5a-1f2b-43ab-9450-d53738848f7f",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    },
                    new UserAspRol()
                    {
                        Id = "bed9b6d3-96d9-4691-b331-eead5b2265e4",
                        Name = "The seller",
                        NormalizedName = "THE SELLER"
                    },
                    new UserAspRol()
                    {
                        Id = "8347125b-b147-4b20-bfcb-1d5a3b55da47",
                        Name = "User",
                        NormalizedName = "USER"
                    }
                );
        }
    }
}
