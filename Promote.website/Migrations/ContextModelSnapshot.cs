﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Promote.website.Models;

#nullable disable

namespace Promote.website.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetailedDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetailedDescriptionBgImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductImageFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tab1Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tab2Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tab3Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("ProductDetailPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DetailedDescriptionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTabSectionIncluded")
                        .HasColumnType("bit");

                    b.Property<string>("Tab1Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tab2Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tab3Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("productDetailPages");
                });

            modelBuilder.Entity("Promote.website.Models.AboutPage", b =>
                {
                    b.Property<int>("AboutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AboutId"), 1L, 1);

                    b.Property<string>("CompanyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageBottom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageTop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissionBgColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisionBgColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs1BgColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs1Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs1Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs2BgColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs2Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs2Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs3BgColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs3Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUs3Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUsSectionBgColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyUsSectionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AboutId");

                    b.ToTable("aboutPages");
                });

            modelBuilder.Entity("Promote.website.Models.Admin", b =>
                {
                    b.Property<int?>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("AdminId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("admins");
                });

            modelBuilder.Entity("Promote.website.Models.ContactForm", b =>
                {
                    b.Property<int>("ContactFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactFormId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactFormId");

                    b.ToTable("contactForms");
                });

            modelBuilder.Entity("Promote.website.Models.ContactPage", b =>
                {
                    b.Property<int>("ContactPageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactPageId"), 1L, 1);

                    b.Property<string>("ContactInfoDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactInfoTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapIframeUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactPageId");

                    b.ToTable("contactPages");
                });

            modelBuilder.Entity("Promote.website.Models.HomePage", b =>
                {
                    b.Property<int>("HomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HomeId"), 1L, 1);

                    b.Property<bool>("IsPopularProductsSectionIncluded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsServicesSectionIncluded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStatisticsSectionIncluded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTaglineSectionIncluded")
                        .HasColumnType("bit");

                    b.Property<int?>("PopularProduct1Id")
                        .HasColumnType("int");

                    b.Property<string>("PopularProduct1Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopularProduct1Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PopularProduct2Id")
                        .HasColumnType("int");

                    b.Property<string>("PopularProduct2Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopularProduct2Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PopularProduct3Id")
                        .HasColumnType("int");

                    b.Property<string>("PopularProduct3Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopularProduct3Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PopularProduct4Id")
                        .HasColumnType("int");

                    b.Property<string>("PopularProduct4Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopularProduct4Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopularProductsSectionBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopularProductsSectionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services1Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services1Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services2Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services2Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services3Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services3Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services4Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services4Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServicesSectionBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServicesSectionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Statistic1Number")
                        .HasColumnType("int");

                    b.Property<string>("Statistic1Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Statistic2Number")
                        .HasColumnType("int");

                    b.Property<string>("Statistic2Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Statistic3Number")
                        .HasColumnType("int");

                    b.Property<string>("Statistic3Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Statistic4Number")
                        .HasColumnType("int");

                    b.Property<string>("Statistic4Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatisticSectionBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tagline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaglineSectionBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HomeId");

                    b.ToTable("homePages");
                });

            modelBuilder.Entity("Promote.website.Models.İnformationForm", b =>
                {
                    b.Property<int>("İnformationFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("İnformationFormId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("İnformationFormId");

                    b.ToTable("İnformationForm");
                });

            modelBuilder.Entity("Promote.website.Models.Layout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FooterColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighlightColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia1Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia1Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia2Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia2Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia3Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia3Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia4Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia4Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia5Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMedia5Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("layouts");
                });

            modelBuilder.Entity("Promote.website.Models.PasswordResetToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("passwordResetTokens");
                });

            modelBuilder.Entity("Promote.website.Models.ProductListPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("productLists");
                });

            modelBuilder.Entity("Promote.website.Models.RelevantDocument", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("relevantDocuments");
                });

            modelBuilder.Entity("Promote.website.Models.Sublink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("sublinks");
                });
#pragma warning restore 612, 618
        }
    }
}
