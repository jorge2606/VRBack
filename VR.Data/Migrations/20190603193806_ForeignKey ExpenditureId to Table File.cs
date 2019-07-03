using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class ForeignKeyExpenditureIdtoTableFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Files_ExpenditureId",
                table: "Files",
                column: "ExpenditureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Expenditures_ExpenditureId",
                table: "Files",
                column: "ExpenditureId",
                principalTable: "Expenditures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Expenditures_ExpenditureId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ExpenditureId",
                table: "Files");
        }
    }
}
