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
                    RoleId = "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb"
                }
            );
        }
    }
}
