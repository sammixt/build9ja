using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace build9ja.infrastructure.Data.Migrations
{
    public partial class BannerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageOne = table.Column<string>(type: "text", nullable: true),
                    TitleOne = table.Column<string>(type: "text", nullable: true),
                    SubTitleOne = table.Column<string>(type: "text", nullable: true),
                    LinkOne = table.Column<string>(type: "text", nullable: true),
                    ImageTwo = table.Column<string>(type: "text", nullable: true),
                    TitleTwo = table.Column<string>(type: "text", nullable: true),
                    SubTitleTwo = table.Column<string>(type: "text", nullable: true),
                    LinkTwo = table.Column<string>(type: "text", nullable: true),
                    ImageThree = table.Column<string>(type: "text", nullable: true),
                    TitleThree = table.Column<string>(type: "text", nullable: true),
                    SubTitleThree = table.Column<string>(type: "text", nullable: true),
                    LinkThree = table.Column<string>(type: "text", nullable: true),
                    ImageFour = table.Column<string>(type: "text", nullable: true),
                    TitleFour = table.Column<string>(type: "text", nullable: true),
                    SubTitleFour = table.Column<string>(type: "text", nullable: true),
                    LinkFour = table.Column<string>(type: "text", nullable: true),
                    SubPageImage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");
        }
    }
}
