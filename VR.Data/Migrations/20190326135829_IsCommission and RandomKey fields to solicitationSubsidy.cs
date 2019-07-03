using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class IsCommissionandRandomKeyfieldstosolicitationSubsidy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCommission",
                table: "SolicitationSubsidies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RandomKey",
                table: "SolicitationSubsidies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommission",
                table: "SolicitationSubsidies");

            migrationBuilder.DropColumn(
                name: "RandomKey",
                table: "SolicitationSubsidies");
        }
    }
}
