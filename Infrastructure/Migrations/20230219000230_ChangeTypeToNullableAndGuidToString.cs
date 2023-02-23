using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangeTypeToNullableAndGuidToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "PasswordHash", "PhotoId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { "feceaf73-c234-42f2-a6d5-2281ded87403", null, "AQAAAAEAACcQAAAAEMN9RhX1LoYPS/XcDHz/8S5eiv2vU2qyxAR4mzARXtBcPtHp7/flsWKx4NOvJ1rIJw==", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebec899-3482-4022-8f8e-33682f877eac",
                column: "ConcurrencyStamp",
                value: "6b28de24-7193-47ec-b19b-69560c91eed5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c8ff47f-9018-4b0b-bbf4-ad7de2e67fbb",
                column: "ConcurrencyStamp",
                value: "6433419d-1612-45b5-b009-a6342c2d02d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c317b51c-07c6-4bb7-a90e-f037c4e91141",
                column: "ConcurrencyStamp",
                value: "5d9d43ed-2d6a-4786-b372-8c998d913daa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da5efb83-e640-473e-8cc2-59e1a49b1741",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "PasswordHash", "PhotoId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { "5d9db703-2bc1-43d3-a88a-5550cf4b301f", new Guid("00000000-0000-0000-0000-000000000000"), "AQAAAAEAACcQAAAAEGWfO/MwvwrJNPGeRaByoWxSPT2eIohEsBKdVJNeypi2Z7uGp7jhaHbjtRceJWZjeA==", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
