using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp_BeTech.Migrations
{
    /// <inheritdoc />
    public partial class updateProductCategorySpecification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Warranting",
                table: "Products",
                newName: "Warranty");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductCategorySpecifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductCategorySpecifications");

            migrationBuilder.RenameColumn(
                name: "Warranty",
                table: "Products",
                newName: "Warranting");
        }
    }
}
