using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternalManagementECommerceTool.Data.Migrations
{
    /// <inheritdoc />
    public partial class OderProductModelUpated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderProducts",
                newName: "Qty");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "OrderProducts",
                newName: "Quantity");
        }
    }
}
