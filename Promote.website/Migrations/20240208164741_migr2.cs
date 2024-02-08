using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promote.website.Migrations
{
    public partial class migr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "relevantDocuments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relevantDocuments", x => x.ID);
                });
            migrationBuilder.AlterColumn<DateTime>(
       name: "CreationDate",
       table: "relevantDocuments",
       type: "date",
       nullable: false,
       oldClrType: typeof(DateTime),
       oldType: "datetime"
   );

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "relevantDocuments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "relevantDocuments");
        }
    }
}
