using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class FixingMaritalSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaritualStatus",
                table: "TMember",
                newName: "MaritalStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaritalStatus",
                table: "TMember",
                newName: "MaritualStatus");
        }
    }
}
