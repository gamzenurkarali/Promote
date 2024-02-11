using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promote.website.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aboutPages",
                columns: table => new
                {
                    AboutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageBottom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUsSectionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUsSectionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs1Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs1Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs1BgColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs2Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs2Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs2BgColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs3Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs3Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhyUs3BgColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aboutPages", x => x.AboutId);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "contactForms",
                columns: table => new
                {
                    ContactFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactForms", x => x.ContactFormId);
                });

            migrationBuilder.CreateTable(
                name: "contactPages",
                columns: table => new
                {
                    ContactPageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfoTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfoDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapIframeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactPages", x => x.ContactPageId);
                });

            migrationBuilder.CreateTable(
                name: "homePages",
                columns: table => new
                {
                    HomeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTaglineSectionIncluded = table.Column<bool>(type: "bit", nullable: false),
                    TaglineSectionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPopularProductsSectionIncluded = table.Column<bool>(type: "bit", nullable: false),
                    PopularProductsSectionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProductsSectionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct1Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct1Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct1Id = table.Column<int>(type: "int", nullable: true),
                    PopularProduct2Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct2Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct2Id = table.Column<int>(type: "int", nullable: true),
                    PopularProduct3Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct3Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct3Id = table.Column<int>(type: "int", nullable: true),
                    PopularProduct4Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct4Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularProduct4Id = table.Column<int>(type: "int", nullable: true),
                    IsServicesSectionIncluded = table.Column<bool>(type: "bit", nullable: false),
                    ServicesSectionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicesSectionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services1Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services1Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services2Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services2Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services3Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services3Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services4Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Services4Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatisticsSectionIncluded = table.Column<bool>(type: "bit", nullable: false),
                    StatisticSectionBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statistic1Number = table.Column<int>(type: "int", nullable: true),
                    Statistic1Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statistic2Number = table.Column<int>(type: "int", nullable: true),
                    Statistic2Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statistic3Number = table.Column<int>(type: "int", nullable: true),
                    Statistic3Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statistic4Number = table.Column<int>(type: "int", nullable: true),
                    Statistic4Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homePages", x => x.HomeId);
                });

            migrationBuilder.CreateTable(
                name: "layouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighlightColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia1Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia1Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia2Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia2Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia3Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia3Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia4Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia4Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia5Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMedia5Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "passwordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passwordResetTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productDetailPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTabSectionIncluded = table.Column<bool>(type: "bit", nullable: false),
                    Tab1Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tab2Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tab3Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailedDescriptionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productDetailPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageHeader = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tab1Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tab2Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tab3Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailedDescriptionBgImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "sublinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sublinks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aboutPages");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "contactForms");

            migrationBuilder.DropTable(
                name: "contactPages");

            migrationBuilder.DropTable(
                name: "homePages");

            migrationBuilder.DropTable(
                name: "layouts");

            migrationBuilder.DropTable(
                name: "passwordResetTokens");

            migrationBuilder.DropTable(
                name: "productDetailPages");

            migrationBuilder.DropTable(
                name: "productLists");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "relevantDocuments");

            migrationBuilder.DropTable(
                name: "sublinks");
        }
    }
}
