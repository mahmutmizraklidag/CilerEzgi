using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CilerEzgi.Migrations
{
    /// <inheritdoc />
    public partial class mobileimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileImage",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileImage",
                table: "Abouts");
        }
    }
}
