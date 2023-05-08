using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangePhotoIdToPhotoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb", "da5efb83-e640-473e-8cc2-59e1a49b1741" });

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c317b51c-07c6-4bb7-a90e-f037c4e91141", "da5efb83-e640-473e-8cc2-59e1a49b1741" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhotoUrl" },
                values: new object[] { "a6eb1cf3-a122-4e98-b2ae-df4ba23b7624", "AQAAAAEAACcQAAAAEOf02NjzwIYDdnd75tqSM3ipT4r1rETTHGNdwS+bGF1zLq9JMVkpEP5qmv37Y6LV/A==", "/api/Documents/Photos/default.png" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c317b51c-07c6-4bb7-a90e-f037c4e91141", "da5efb83-e640-473e-8cc2-59e1a49b1741" });

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebec899-3482-4022-8f8e-33682f877eac",
                column: "ConcurrencyStamp",
                value: "10fcaa20-02b0-43b7-9cee-87919145266b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb",
                column: "ConcurrencyStamp",
                value: "3654daee-c2b0-4569-89e5-c510106229b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c317b51c-07c6-4bb7-a90e-f037c4e91141",
                column: "ConcurrencyStamp",
                value: "59bfaba3-91a0-44bd-ba5d-2967ad10d4a2");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb", "da5efb83-e640-473e-8cc2-59e1a49b1741" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "feceaf73-c234-42f2-a6d5-2281ded87403", "AQAAAAEAACcQAAAAEMN9RhX1LoYPS/XcDHz/8S5eiv2vU2qyxAR4mzARXtBcPtHp7/flsWKx4NOvJ1rIJw==" });
        }
    }
}
