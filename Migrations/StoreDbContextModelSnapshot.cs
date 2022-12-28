﻿// <auto-generated />
using System;
using LampStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LampStore.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LampStore.Models.AboutPage", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("ID"), 1L, 1);

                    b.Property<bool>("DisplayHomePage")
                        .HasColumnType("bit");

                    b.Property<string>("HeadingOneNode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadingTwoNode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgOneUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgTwoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParagraphOneNode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParagraphTwoNode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoOneUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoTwoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AboutPages");
                });

            modelBuilder.Entity("LampStore.Models.CartLine", b =>
                {
                    b.Property<int>("CartLineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartLineID"), 1L, 1);

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<long>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartLineID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartLine");
                });

            modelBuilder.Entity("LampStore.Models.Category", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("ID"), 1L, 1);

                    b.Property<string>("CategoryImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DisplayHomePage")
                        .HasColumnType("bit");

                    b.Property<bool>("DisplaySlider")
                        .HasColumnType("bit");

                    b.Property<string>("HeadingThree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadingTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgThreeUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgTwoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ParentID")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Slider")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Categorys");
                });

            modelBuilder.Entity("LampStore.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GiftWrap")
                        .HasColumnType("bit");

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Shipped")
                        .HasColumnType("bit");

                    b.Property<byte>("StatusOrders")
                        .HasColumnType("tinyint");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LampStore.Models.Product", b =>
                {
                    b.Property<long?>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("ProductID"), 1L, 1);

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<string>("BaseSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CordLength")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Kelvins")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LightSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MinDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OldPrice")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParentCategoryId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Photos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PowerW")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LampStore.Models.CartLine", b =>
                {
                    b.HasOne("LampStore.Models.Order", null)
                        .WithMany("Lines")
                        .HasForeignKey("OrderID");

                    b.HasOne("LampStore.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LampStore.Models.Product", b =>
                {
                    b.HasOne("LampStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LampStore.Models.Order", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
