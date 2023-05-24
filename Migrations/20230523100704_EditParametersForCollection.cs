using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class EditParametersForCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalBlocksForModelLight_LightsModels_ModelLightID",
                table: "AdditionalBlocksForModelLight");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_ProductTagsID",
                table: "ProductTag");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TypeProducts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Tags",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductTagsID",
                table: "ProductTag",
                newName: "ProductTagsId");

            migrationBuilder.RenameColumn(
                name: "ModelLightID",
                table: "AdditionalBlocksForModelLight",
                newName: "ModelLightId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AdditionalBlocksForModelLight",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalBlocksForModelLight_ModelLightID",
                table: "AdditionalBlocksForModelLight",
                newName: "IX_AdditionalBlocksForModelLight_ModelLightId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AdditionalBlocksForCollection",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalBlocksForModelLight_LightsModels_ModelLightId",
                table: "AdditionalBlocksForModelLight",
                column: "ModelLightId",
                principalTable: "LightsModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_ProductTagsId",
                table: "ProductTag",
                column: "ProductTagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalBlocksForModelLight_LightsModels_ModelLightId",
                table: "AdditionalBlocksForModelLight");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_ProductTagsId",
                table: "ProductTag");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TypeProducts",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tags",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ProductTagsId",
                table: "ProductTag",
                newName: "ProductTagsID");

            migrationBuilder.RenameColumn(
                name: "ModelLightId",
                table: "AdditionalBlocksForModelLight",
                newName: "ModelLightID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AdditionalBlocksForModelLight",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalBlocksForModelLight_ModelLightId",
                table: "AdditionalBlocksForModelLight",
                newName: "IX_AdditionalBlocksForModelLight_ModelLightID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AdditionalBlocksForCollection",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalBlocksForModelLight_LightsModels_ModelLightID",
                table: "AdditionalBlocksForModelLight",
                column: "ModelLightID",
                principalTable: "LightsModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_ProductTagsID",
                table: "ProductTag",
                column: "ProductTagsID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
