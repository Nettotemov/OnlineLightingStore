using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class MetaDataAddedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "MetaDatas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetaDatas_ProductId",
                table: "MetaDatas",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MetaDatas_Products_ProductId",
                table: "MetaDatas",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetaDatas_Products_ProductId",
                table: "MetaDatas");

            migrationBuilder.DropIndex(
                name: "IX_MetaDatas_ProductId",
                table: "MetaDatas");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "MetaDatas");
        }
    }
}
