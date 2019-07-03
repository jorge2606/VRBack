using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class addfieldSupervisorId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupervisorId2",
                table: "SolicitationStates",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId2",
                table: "SolicitationStates");
        }
    }
}
