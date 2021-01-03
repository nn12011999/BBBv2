using Microsoft.EntityFrameworkCore.Migrations;

namespace BBB.Data.Migrations
{
    public partial class EditTable_FileSave_thumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumnail",
                table: "FileSaves",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumnail",
                table: "FileSaves");
        }
    }
}
