using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Data.Migrations
{
    public partial class addfieldPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Audit_Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Audit_Users");
        }
    }
}
