using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Data.Migrations
{
    public partial class fieldUserIdinAudit_Useradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Audit_Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Audit_Users");
        }
    }
}
