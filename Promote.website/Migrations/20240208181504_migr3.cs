using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promote.website.Migrations
{
    public partial class migr3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialMedia5Icon",
                table: "layouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialMedia5Link",
                table: "layouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMedia5Icon",
                table: "layouts");

            migrationBuilder.DropColumn(
                name: "SocialMedia5Link",
                table: "layouts");
        }
    }
}
