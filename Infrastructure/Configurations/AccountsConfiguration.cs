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
            var receptionistUser = new Account
            {
                Id = "da5efb83-e640-473e-8cc2-59e1a49b1741",
                UserName = "octopus",
                NormalizedUserName = "OCTOPUS",
                Email = "octo@gmail.com",
                NormalizedEmail = "OCTO@GMAIL.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                PhotoUrl = "/api/documents/Photos/2c8a3603-e40f-461d-ba4f-1b1f4da51e06.png",
                SecurityStamp = "1234567890"
            };
            var doctorUser = new Account
            {
                Id = "de5efb13-e610-471e-5cc2-59e1a49a1741",
                UserName = "doctor12",
                NormalizedUserName = "DOCTOR12",
                Email = "doctor12@gmail.com",
                NormalizedEmail = "DOCTOR12@GMAIL.COM",
                PhoneNumber = "1234567890",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                PhotoUrl = "/api/documents/Photos/2c8a3603-e40f-461d-ba4f-1b1f4da51e06.png",
                SecurityStamp = "1234567890"
            };
            var passHash = new PasswordHasher<Account>();
            doctorUser.PasswordHash = passHash.HashPassword(doctorUser, "password");
            receptionistUser.PasswordHash = passHash.HashPassword(receptionistUser, "password");
            builder.HasData(doctorUser, receptionistUser);
        }
    }
}
