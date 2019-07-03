using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class AddfieldOrderReportToTableApproveOfAuthorityThatOrderCommission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderReport",
                table: "ApproveOfAuthorityThatOrderCommissions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderReport",
                table: "ApproveOfAuthorityThatOrderCommissions");
        }
    }
}
