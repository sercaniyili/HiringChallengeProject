using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiringChallange.Persistence.Migrations
{
    public partial class otoidentityref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Categories_CategoryId1",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_CategoryId1",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "ShoppingLists");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "ShoppingLists",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_CategoryId",
                table: "ShoppingLists",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Categories_CategoryId",
                table: "ShoppingLists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Categories_CategoryId",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_CategoryId",
                table: "ShoppingLists");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ShoppingLists",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId1",
                table: "ShoppingLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_CategoryId1",
                table: "ShoppingLists",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Categories_CategoryId1",
                table: "ShoppingLists",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
