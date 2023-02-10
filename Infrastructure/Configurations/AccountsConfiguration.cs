using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AccountsConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            var user = new Account
            {
                Id = "da5efb83-e640-473e-8cc2-59e1a49b1741",
                UserName = "octopus",
                NormalizedUserName = "OCTOPUS",
                Email = "octo@gmail.com",
                NormalizedEmail = "OCTO@GMAIL.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = "1234567890"
            };
            var passHash = new PasswordHasher<Account>();
            user.PasswordHash = passHash.HashPassword(user, "password");
            builder.HasData(user);
        }
    }
}
