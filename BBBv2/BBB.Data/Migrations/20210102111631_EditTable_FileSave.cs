using Microsoft.EntityFrameworkCore.Migrations;

namespace BBB.Data.Migrations
{
    public partial class EditTable_FileSave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FileSaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FileSaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSaves_CategoryId",
                table: "FileSaves",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSaves_Categories_CategoryId",
                table: "FileSaves",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSaves_Categories_CategoryId",
                table: "FileSaves");

            migrationBuilder.DropIndex(
                name: "IX_FileSaves_CategoryId",
                table: "FileSaves");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FileSaves");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FileSaves");
        }
    }
}
