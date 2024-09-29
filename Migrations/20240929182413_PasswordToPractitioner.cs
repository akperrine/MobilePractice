using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilePractice.Migrations
{
    /// <inheritdoc />
    public partial class PasswordToPractitioner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Practitioners",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Practitioners");
        }
    }
}
