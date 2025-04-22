using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class AddingLoanAndRecoveryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TLoan",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    LoanType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LoanAmount = table.Column<double>(type: "float", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    RateOfInterest = table.Column<float>(type: "real", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDisbursed = table.Column<bool>(type: "bit", nullable: false),
                    DisbursedOn = table.Column<DateOnly>(type: "date", nullable: false),
                    DisbursedVia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NetAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLoan", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_TLoan_TMember_MemberId",
                        column: x => x.MemberId,
                        principalTable: "TMember",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRecovery",
                columns: table => new
                {
                    RecoveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    InstallmentNo = table.Column<int>(type: "int", nullable: false),
                    InstallmentAmount = table.Column<double>(type: "float", nullable: false),
                    PrincipalInstallment = table.Column<double>(type: "float", nullable: false),
                    LoanInstallment = table.Column<double>(type: "float", nullable: false),
                    RecoveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RecoveryOn = table.Column<DateOnly>(type: "date", nullable: false),
                    RecoveryDay = table.Column<int>(type: "int", nullable: false),
                    RecoveredVia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecoveredBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRecovery", x => x.RecoveryId);
                    table.ForeignKey(
                        name: "FK_TRecovery_TLoan_LoanId",
                        column: x => x.LoanId,
                        principalTable: "TLoan",
                        principalColumn: "LoanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TLoan_MemberId",
                table: "TLoan",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TRecovery_LoanId",
                table: "TRecovery",
                column: "LoanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRecovery");

            migrationBuilder.DropTable(
                name: "TLoan");
        }
    }
}
