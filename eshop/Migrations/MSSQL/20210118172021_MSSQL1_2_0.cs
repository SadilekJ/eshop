using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations.MSSQL
{
    public partial class MSSQL1_2_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTimeCreated = table.Column<DateTime>(nullable: false),
                    CommentMessage = table.Column<string>(maxLength: 256, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MainComment_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainComment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTimeCreated = table.Column<DateTime>(nullable: false),
                    CommentMessage = table.Column<string>(maxLength: 256, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    MainCommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubComment_MainComment_MainCommentId",
                        column: x => x.MainCommentId,
                        principalTable: "MainComment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubComment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_UserId",
                table: "MainComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubComment_MainCommentId",
                table: "SubComment",
                column: "MainCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubComment_UserId",
                table: "SubComment",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubComment");

            migrationBuilder.DropTable(
                name: "MainComment");
        }
    }
}
