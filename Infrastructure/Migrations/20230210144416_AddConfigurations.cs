using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ebec899-3482-4022-8f8e-33682f877eac", "6b28de24-7193-47ec-b19b-69560c91eed5", "Doctor", "DOCTOR" },
                    { "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb", "6433419d-1612-45b5-b009-a6342c2d02d2", "Patient", "PATIENT" },
                    { "c317b51c-07c6-4bb7-a90e-f037c4e91141", "5d9d43ed-2d6a-4786-b372-8c998d913daa", "Receptionist", "RECEPTIONIST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoId", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { "da5efb83-e640-473e-8cc2-59e1a49b1741", 0, "5d9db703-2bc1-43d3-a88a-5550cf4b301f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "octo@gmail.com", false, false, null, "OCTO@GMAIL.COM", "OCTOPUS", "AQAAAAEAACcQAAAAEGWfO/MwvwrJNPGeRaByoWxSPT2eIohEsBKdVJNeypi2Z7uGp7jhaHbjtRceJWZjeA==", "1234567890", false, new Guid("00000000-0000-0000-0000-000000000000"), "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "octopus" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb", "da5efb83-e640-473e-8cc2-59e1a49b1741" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebec899-3482-4022-8f8e-33682f877eac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c317b51c-07c6-4bb7-a90e-f037c4e91141");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb", "da5efb83-e640-473e-8cc2-59e1a49b1741" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741");
        }
    }
}
