using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffe_Shop_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Ab1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Product_Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Product_Orders",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}
