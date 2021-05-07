using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShoppingMall.Data.Migrations
{
    public partial class changes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "PhotoProducts");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "PhotoProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PhotoProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "PhotoProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "PhotoProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "PhotoProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
