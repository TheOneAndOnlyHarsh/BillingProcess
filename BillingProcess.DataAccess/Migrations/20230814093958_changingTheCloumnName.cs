using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingProcess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changingTheCloumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPaid",
                table: "Bills",
                newName: "IsPaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Bills",
                newName: "IdPaid");
        }
    }
}
