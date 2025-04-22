using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Migrations
{
    /// <inheritdoc />
    public partial class AddingLoanToMemberObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TLoan_MemberId",
                table: "TLoan");

            migrationBuilder.CreateIndex(
                name: "IX_TLoan_MemberId",
                table: "TLoan",
                column: "MemberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TLoan_MemberId",
                table: "TLoan");

            migrationBuilder.CreateIndex(
                name: "IX_TLoan_MemberId",
                table: "TLoan",
                column: "MemberId");
        }
    }
}
