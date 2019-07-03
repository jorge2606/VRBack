using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit_Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDate = table.Column<DateTime>(nullable: false),
                    AuditUser = table.Column<string>(nullable: true),
                    TextData = table.Column<string>(nullable: true),
                    Tittle = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: false),
                    Read = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_Notifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit_Notifications");
        }
    }
}
