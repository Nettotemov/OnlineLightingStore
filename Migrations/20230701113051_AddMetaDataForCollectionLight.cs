using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class AddMetaDataForCollectionLight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionLightId",
                table: "MetaDatas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetaDatas_CollectionLightId",
                table: "MetaDatas",
                column: "CollectionLightId",
                unique: true,
                filter: "[CollectionLightId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MetaDatas_CollectionLights_CollectionLightId",
                table: "MetaDatas",
                column: "CollectionLightId",
                principalTable: "CollectionLights",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetaDatas_CollectionLights_CollectionLightId",
                table: "MetaDatas");

            migrationBuilder.DropIndex(
                name: "IX_MetaDatas_CollectionLightId",
                table: "MetaDatas");

            migrationBuilder.DropColumn(
                name: "CollectionLightId",
                table: "MetaDatas");
        }
    }
}
