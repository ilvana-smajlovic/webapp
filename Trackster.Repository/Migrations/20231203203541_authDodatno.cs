using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trackster.Repository.Migrations
{
    /// <inheritdoc />
    public partial class authDodatno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "twoFCode",
                table: "authenticationTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "twoFIsUnlocked",
                table: "authenticationTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "twoFCode",
                table: "authenticationTokens");

            migrationBuilder.DropColumn(
                name: "twoFIsUnlocked",
                table: "authenticationTokens");
        }
    }
}
