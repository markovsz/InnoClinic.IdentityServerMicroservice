using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "4ebec899-3482-4022-8f8e-33682f877eac",
                    Name = "Doctor",
                    NormalizedName = "DOCTOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb",
                    Name = "Patient",
                    NormalizedName = "PATIENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "c317b51c-07c6-4bb7-a90e-f037c4e91141",
                    Name = "Receptionist",
                    NormalizedName = "RECEPTIONIST",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }
    }
}
