using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class IsImportantandCanRepeattoExpenditureType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanRepeat",
                table: "ExpenditureTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsImportant",
                table: "ExpenditureTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanRepeat",
                table: "ExpenditureTypes");

            migrationBuilder.DropColumn(
                name: "IsImportant",
                table: "ExpenditureTypes");
        }
    }
}
