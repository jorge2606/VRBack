using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Data.Migrations
{
    public partial class addcreatorUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatorUserId",
                table: "Audit_Notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Audit_Notifications");
        }
    }
}
