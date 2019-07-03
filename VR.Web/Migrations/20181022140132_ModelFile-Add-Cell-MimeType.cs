using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Web.Migrations
{
    public partial class ModelFileAddCellMimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "Files");
        }
    }
}
