using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class addfielIsRepaymenttoSolicitationSubsidyandCostRepaymenttoDestiny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRepayment",
                table: "SolicitationSubsidies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "CostRepayment",
                table: "Destinies",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRepayment",
                table: "SolicitationSubsidies");

            migrationBuilder.DropColumn(
                name: "CostRepayment",
                table: "Destinies");
        }
    }
}
