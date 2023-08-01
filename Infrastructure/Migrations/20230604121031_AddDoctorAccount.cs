using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDoctorAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebec899-3482-4022-8f8e-33682f877eac",
                column: "ConcurrencyStamp",
                value: "068b9f18-03ba-4769-af9e-9f03ec21988c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb",
                column: "ConcurrencyStamp",
                value: "96f65817-7bee-4ba0-92a7-9033ecb5795b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c317b51c-07c6-4bb7-a90e-f037c4e91141",
                column: "ConcurrencyStamp",
                value: "18996483-14f4-4d96-9404-36fc13e42779");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhotoUrl" },
                values: new object[] { "913bd5c4-0e6f-4476-9a2c-fbed18916d77", "AQAAAAEAACcQAAAAEKrIcEfy37DDEZ7FUUA1uPfYbqlQ+TcwMsziRXYbdVkHSSUGpA1LErDzLbV1XhDC3Q==", "/api/documents/Photos/2c8a3603-e40f-461d-ba4f-1b1f4da51e06.png" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoUrl", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { "de5efb13-e610-471e-5cc2-59e1a49a1741", 0, "0e44c263-c389-4c41-a870-19eb5262c753", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "doctor12@gmail.com", false, false, null, "DOCTOR12@GMAIL.COM", "DOCTOR12", "AQAAAAEAACcQAAAAEO7vZvucgkPHmKDUpnh7Y0tAkpYGtrzr92qagI9nAH12kjyIxV5h1nYn0BZ1+eqNzA==", "1234567890", false, "/api/documents/Photos/2c8a3603-e40f-461d-ba4f-1b1f4da51e06.png", "1234567890", false, null, null, "doctor12" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4ebec899-3482-4022-8f8e-33682f877eac", "de5efb13-e610-471e-5cc2-59e1a49a1741" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ebec899-3482-4022-8f8e-33682f877eac", "de5efb13-e610-471e-5cc2-59e1a49a1741" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de5efb13-e610-471e-5cc2-59e1a49a1741");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebec899-3482-4022-8f8e-33682f877eac",
                column: "ConcurrencyStamp",
                value: "f1385511-6824-4403-92c9-8ad11e336a55");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb",
                column: "ConcurrencyStamp",
                value: "fc8bfef3-4b7e-459a-8f00-559f386a2197");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c317b51c-07c6-4bb7-a90e-f037c4e91141",
                column: "ConcurrencyStamp",
                value: "4e136fe5-64e3-4728-ac62-dfbe18703cc0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhotoUrl" },
                values: new object[] { "a6eb1cf3-a122-4e98-b2ae-df4ba23b7624", "AQAAAAEAACcQAAAAEOf02NjzwIYDdnd75tqSM3ipT4r1rETTHGNdwS+bGF1zLq9JMVkpEP5qmv37Y6LV/A==", "/api/Documents/Photos/default.png" });
        }
    }
}
