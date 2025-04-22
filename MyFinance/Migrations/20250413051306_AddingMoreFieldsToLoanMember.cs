using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class AddingMoreFieldsToLoanMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RecoveredAmount",
                table: "TRecovery",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ResidenceStatus",
                table: "TMember",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "FamilyExpenditure",
                table: "TLoan",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FamilyIncome",
                table: "TLoan",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "TLoan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NoOfFamilyNumbers",
                table: "TLoan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Obligation",
                table: "TLoan",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfLoan",
                table: "TLoan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Surplus",
                table: "TLoan",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveredAmount",
                table: "TRecovery");

            migrationBuilder.DropColumn(
                name: "ResidenceStatus",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "FamilyExpenditure",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "FamilyIncome",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "NoOfFamilyNumbers",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "Obligation",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "PurposeOfLoan",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "Surplus",
                table: "TLoan");
        }
    }
}
