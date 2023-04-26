using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "da5efb83-e640-473e-8cc2-59e1a49b1741",
                    RoleId = "c317b51c-07c6-4bb7-a90e-f037c4e91141"
                }
            );
        }
    }
}
