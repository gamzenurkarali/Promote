using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promote.website.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelevantDocument",
                table: "RelevantDocument");

            migrationBuilder.RenameTable(
                name: "RelevantDocument",
                newName: "relevantDocuments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_relevantDocuments",
                table: "relevantDocuments",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "İnformationForm",
                columns: table => new
                {
                    İnformationFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_İnformationForm", x => x.İnformationFormId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "İnformationForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_relevantDocuments",
                table: "relevantDocuments");

            migrationBuilder.RenameTable(
                name: "relevantDocuments",
                newName: "RelevantDocument");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelevantDocument",
                table: "RelevantDocument",
                column: "ID");
        }
    }
}
