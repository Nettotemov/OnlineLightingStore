using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class CooperNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MinDescription",
                table: "CooperationPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
