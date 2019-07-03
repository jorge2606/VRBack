using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class addfieldPercentageandOutsideProvinceandPercentagetoTableExpenditure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OutsideProvince",
                table: "ExpenditureTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "ExpenditureTypes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Expenditures",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutsideProvince",
                table: "ExpenditureTypes");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "ExpenditureTypes");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Expenditures");
        }
    }
}
