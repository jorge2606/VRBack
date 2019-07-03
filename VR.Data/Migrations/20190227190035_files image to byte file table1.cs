using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class filesimagetobytefiletable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Files");


            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenditureId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Expenditures",
                nullable: true);
        }
    }
}
