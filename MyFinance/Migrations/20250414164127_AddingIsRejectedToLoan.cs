using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsRejectedToLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "TLoan",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "TLoan");
        }
    }
}
