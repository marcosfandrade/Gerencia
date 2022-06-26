using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerencia.API.Migrations.SqliteMigrations
{
    public partial class ChangeUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
