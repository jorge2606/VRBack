using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class removenullfromfieldIsCommissiontoCreateSolicitationSubsidy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsCommission",
                table: "SolicitationSubsidies",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsCommission",
                table: "SolicitationSubsidies",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
