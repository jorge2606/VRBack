using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Data.Migrations
{
    public partial class addtableaudit_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDate = table.Column<DateTime>(nullable: false),
                    AuditUser = table.Column<string>(nullable: true),
                    Dni = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit_Users");
        }
    }
}
