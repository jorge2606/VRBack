using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class addLegalRulingIdToSolicitationSubsidy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LegalRulingId",
                table: "SolicitationSubsidies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitationSubsidies_LegalRulingId",
                table: "SolicitationSubsidies",
                column: "LegalRulingId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitationSubsidies_LegalRulings_LegalRulingId",
                table: "SolicitationSubsidies",
                column: "LegalRulingId",
                principalTable: "LegalRulings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitationSubsidies_LegalRulings_LegalRulingId",
                table: "SolicitationSubsidies");

            migrationBuilder.DropIndex(
                name: "IX_SolicitationSubsidies_LegalRulingId",
                table: "SolicitationSubsidies");

            migrationBuilder.DropColumn(
                name: "LegalRulingId",
                table: "SolicitationSubsidies");
        }
    }
}
