using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class AddingRecoveryDayAndDateToLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecoveryDate",
                table: "TLoan",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecoveryDay",
                table: "TLoan",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryDate",
                table: "TLoan");

            migrationBuilder.DropColumn(
                name: "RecoveryDay",
                table: "TLoan");
        }
    }
}
