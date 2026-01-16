using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CilerEzgi.Migrations
{
    /// <inheritdoc />
    public partial class added_ispay_to_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPay",
                table: "Orders");
        }
    }
}
