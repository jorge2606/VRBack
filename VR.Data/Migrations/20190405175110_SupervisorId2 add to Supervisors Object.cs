using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class SupervisorId2addtoSupervisorsObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SupervisorUserAgents_SupervisorId2",
                table: "SupervisorUserAgents",
                column: "SupervisorId2");

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorUserAgents_AspNetUsers_SupervisorId2",
                table: "SupervisorUserAgents",
                column: "SupervisorId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorUserAgents_AspNetUsers_SupervisorId2",
                table: "SupervisorUserAgents");

            migrationBuilder.DropIndex(
                name: "IX_SupervisorUserAgents_SupervisorId2",
                table: "SupervisorUserAgents");
        }
    }
}
