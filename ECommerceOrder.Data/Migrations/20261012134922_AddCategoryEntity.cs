using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceOrderApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "barcode",
                table: "Products",
                newName: "Barcode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "Products",
                newName: "barcode");
        }
    }
}
