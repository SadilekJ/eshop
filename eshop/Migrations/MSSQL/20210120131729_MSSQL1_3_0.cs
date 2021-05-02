using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations.MSSQL
{
    public partial class MSSQL1_3_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "SubComment",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Product",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "MainComment",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "SubComment",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Product",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "MainComment",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment",
                column: "ProductId",
                unique: true);
        }
    }
}
