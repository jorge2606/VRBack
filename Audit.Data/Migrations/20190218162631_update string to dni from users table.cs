using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Data.Migrations
{
    public partial class updatestringtodnifromuserstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "Audit_Users",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Dni",
                table: "Audit_Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
