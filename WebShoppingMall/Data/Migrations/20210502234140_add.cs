using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShoppingMall.Data.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "PhotoProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "PhotoProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
