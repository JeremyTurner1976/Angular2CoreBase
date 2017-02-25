using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular2CoreBase.Data.Migrations
{
    public partial class CoreBase002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleInitial",
                table: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Errors",
                newName: "CreatedDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "Errors",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "MiddleInitial",
                table: "ApplicationUsers",
                nullable: true);
        }
    }
}
