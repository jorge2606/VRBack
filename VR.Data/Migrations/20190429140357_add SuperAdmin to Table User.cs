using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class addSuperAdmintoTableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FinalizeDate",
                table: "SolicitationSubsidies",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SolicitationSubsidies",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<bool>(
                name: "SuperAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuperAdmin",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinalizeDate",
                table: "SolicitationSubsidies",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SolicitationSubsidies",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
