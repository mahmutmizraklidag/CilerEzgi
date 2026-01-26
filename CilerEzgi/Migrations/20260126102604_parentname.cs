using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CilerEzgi.Migrations
{
    /// <inheritdoc />
    public partial class parentname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentPricingName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentPricingName",
                table: "Orders");
        }
    }
}
