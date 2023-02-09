using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutPages",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgOneUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoOneUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadingOneNode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParagraphOneNode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgTwoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoTwoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadingTwoNode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParagraphTwoNode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayHomePage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentID = table.Column<long>(type: "bigint", nullable: false),
                    HeadingTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgTwoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadingThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgThreeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplaySlider = table.Column<bool>(type: "bit", nullable: false),
                    DisplayHomePage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CooperationPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCooperation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CooperationImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperationPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InfoPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextForBanner = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SvgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftWrap = table.Column<bool>(type: "bit", nullable: false),
                    Shipped = table.Column<bool>(type: "bit", nullable: false),
                    StatusOrders = table.Column<byte>(type: "tinyint", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSettings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Setting = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProducts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InfoProp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfoId = table.Column<int>(type: "int", nullable: false),
                    TypesAddintionalFields = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoProp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InfoProp_InfoPages_InfoId",
                        column: x => x.InfoId,
                        principalTable: "InfoPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artikul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kelvins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    OldPrice = table.Column<long>(type: "bigint", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CordLength = table.Column<int>(type: "int", nullable: true),
                    LightSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PowerW = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_TypeProducts_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "TypeProducts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    CartLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.CartLineID);
                    table.ForeignKey(
                        name: "FK_CartLine_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_CartLine_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductTagsID = table.Column<int>(type: "int", nullable: false),
                    ProductsProductID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductTagsID, x.ProductsProductID });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductsProductID",
                        column: x => x.ProductsProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_ProductTagsID",
                        column: x => x.ProductTagsID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_OrderID",
                table: "CartLine",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_ProductID",
                table: "CartLine",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_InfoProp_InfoId",
                table: "InfoProp",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_ProductsProductID",
                table: "ProductTag",
                column: "ProductsProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutPages");

            migrationBuilder.DropTable(
                name: "CartLine");

            migrationBuilder.DropTable(
                name: "CooperationPages");

            migrationBuilder.DropTable(
                name: "InfoProp");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "InfoPages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropTable(
                name: "TypeProducts");
        }
    }
}
