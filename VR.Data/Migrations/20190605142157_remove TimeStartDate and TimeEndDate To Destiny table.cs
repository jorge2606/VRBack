using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class removeTimeStartDateandTimeEndDateToDestinytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeEndDate",
                table: "Destinies");

            migrationBuilder.RenameColumn(
                name: "TimeStartDate",
                table: "Destinies",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Destinies",
                newName: "TimeStartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeEndDate",
                table: "Destinies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
