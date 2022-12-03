using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class DisplayHomePage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionThree",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionTwo",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayHomePage",
                table: "Categorys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisplaySlider",
                table: "Categorys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HeadingThree",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadingTwo",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgThreeUrl",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgTwoUrl",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slider",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayHomePage",
                table: "AboutPages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionThree",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "DescriptionTwo",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "DisplayHomePage",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "DisplaySlider",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "HeadingThree",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "HeadingTwo",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "ImgThreeUrl",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "ImgTwoUrl",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "Slider",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "DisplayHomePage",
                table: "AboutPages");
        }
    }
}
