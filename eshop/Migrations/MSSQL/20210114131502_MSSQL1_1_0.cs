using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations.MSSQL
{
    public partial class MSSQL1_1_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarouselContent",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DataTarget",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "DetailOfProduct",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailOfProduct",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "CarouselContent",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataTarget",
                table: "Product",
                nullable: false,
                defaultValue: "");
        }
    }
}
