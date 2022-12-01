using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LampStore.Migrations
{
    public partial class AboutPage : Migration
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
                    ParagraphTwoNode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutPages", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutPages");
        }
    }
}
