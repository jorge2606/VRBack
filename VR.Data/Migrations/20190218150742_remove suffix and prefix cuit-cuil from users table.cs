using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class removesuffixandprefixcuitcuilfromuserstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrefixCuil",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuffixCuil",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrefixCuil",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuffixCuil",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
