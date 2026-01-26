using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CilerEzgi.Migrations
{
    /// <inheritdoc />
    public partial class pricingparent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Pricings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Pricings");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Services",
                type: "int",
                nullable: true);
        }
    }
}
