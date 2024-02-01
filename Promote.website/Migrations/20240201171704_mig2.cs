using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promote.website.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoPath",
                table: "sublinks",
                newName: "PagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagePath",
                table: "sublinks",
                newName: "LogoPath");
        }
    }
}
