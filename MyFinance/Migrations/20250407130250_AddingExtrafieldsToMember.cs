using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class AddingExtrafieldsToMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CasteAndReligion",
                table: "TMember",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "TMember",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "TMember",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HusbandName",
                table: "TMember",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Income",
                table: "TMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "TMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IsApprovedBy",
                table: "TMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsOnboardedBy",
                table: "TMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritualStatus",
                table: "TMember",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "TMember",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PANNumber",
                table: "TMember",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisbursedBy",
                table: "TLoan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasteAndReligion",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "HusbandName",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "IsApprovedBy",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "IsOnboardedBy",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "MaritualStatus",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "PANNumber",
                table: "TMember");

            migrationBuilder.DropColumn(
                name: "DisbursedBy",
                table: "TLoan");
        }
    }
}
