using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class ChangenametoTableApproveOfAuthorityThatOrderCommission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AprOfAuthorityThatOrderCommissions",
                table: "AprOfAuthorityThatOrderCommissions");

            migrationBuilder.RenameTable(
                name: "AprOfAuthorityThatOrderCommissions",
                newName: "ApproveOfAuthorityThatOrderCommissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApproveOfAuthorityThatOrderCommissions",
                table: "ApproveOfAuthorityThatOrderCommissions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApproveOfAuthorityThatOrderCommissions",
                table: "ApproveOfAuthorityThatOrderCommissions");

            migrationBuilder.RenameTable(
                name: "ApproveOfAuthorityThatOrderCommissions",
                newName: "AprOfAuthorityThatOrderCommissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AprOfAuthorityThatOrderCommissions",
                table: "AprOfAuthorityThatOrderCommissions",
                column: "Id");
        }
    }
}
