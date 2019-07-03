using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class SupervisorId2andSupervisorIdnulleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorUserAgents_AspNetUsers_SupervisorId",
                table: "SupervisorUserAgents");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupervisorId2",
                table: "SupervisorUserAgents",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "SupervisorId",
                table: "SupervisorUserAgents",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorUserAgents_AspNetUsers_SupervisorId",
                table: "SupervisorUserAgents",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorUserAgents_AspNetUsers_SupervisorId",
                table: "SupervisorUserAgents");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupervisorId2",
                table: "SupervisorUserAgents",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupervisorId",
                table: "SupervisorUserAgents",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorUserAgents_AspNetUsers_SupervisorId",
                table: "SupervisorUserAgents",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
