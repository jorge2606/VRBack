using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class AddTableApproveOfAuthority_Solicitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproveOfAuthoritySolicitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SolicitationSubsidyId = table.Column<Guid>(nullable: false),
                    AprApproveOfAuthorityThatOrderCommissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveOfAuthoritySolicitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproveOfAuthoritySolicitations_ApproveOfAuthorityThatOrderCommissions_AprApproveOfAuthorityThatOrderCommissionId",
                        column: x => x.AprApproveOfAuthorityThatOrderCommissionId,
                        principalTable: "ApproveOfAuthorityThatOrderCommissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApproveOfAuthoritySolicitations_SolicitationSubsidies_SolicitationSubsidyId",
                        column: x => x.SolicitationSubsidyId,
                        principalTable: "SolicitationSubsidies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApproveOfAuthoritySolicitations_AprApproveOfAuthorityThatOrderCommissionId",
                table: "ApproveOfAuthoritySolicitations",
                column: "AprApproveOfAuthorityThatOrderCommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveOfAuthoritySolicitations_SolicitationSubsidyId",
                table: "ApproveOfAuthoritySolicitations",
                column: "SolicitationSubsidyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApproveOfAuthoritySolicitations");
        }
    }
}
