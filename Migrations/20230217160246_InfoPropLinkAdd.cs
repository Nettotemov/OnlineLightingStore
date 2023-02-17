using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class InfoPropLinkAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfoPropLink",
                table: "InfoProp",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoPropLink",
                table: "InfoProp");
        }
    }
}
