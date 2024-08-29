using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp_BeTech.Migrations
{
    /// <inheritdoc />
    public partial class updatepropertyOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderItems");
        }
    }
}
