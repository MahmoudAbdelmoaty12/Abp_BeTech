using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp_BeTech.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Warrating",
                table: "Products",
                newName: "Warranting");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Warranting",
                table: "Products",
                newName: "Warrating");
        }
    }
}
