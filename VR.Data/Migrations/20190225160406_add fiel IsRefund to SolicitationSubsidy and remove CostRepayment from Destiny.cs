using Microsoft.EntityFrameworkCore.Migrations;

namespace VR.Data.Migrations
{
    public partial class addfielIsRefundtoSolicitationSubsidyandremoveCostRepaymentfromDestiny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRepayment",
                table: "SolicitationSubsidies",
                newName: "IsRefund");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRefund",
                table: "SolicitationSubsidies",
                newName: "IsRepayment");
        }
    }
}
