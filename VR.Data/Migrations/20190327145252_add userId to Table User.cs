using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class adduserIdtoTableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Expenditures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenditures_UserId",
                table: "Expenditures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_AspNetUsers_UserId",
                table: "Expenditures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_AspNetUsers_UserId",
                table: "Expenditures");

            migrationBuilder.DropIndex(
                name: "IX_Expenditures_UserId",
                table: "Expenditures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expenditures");
        }
    }
}
