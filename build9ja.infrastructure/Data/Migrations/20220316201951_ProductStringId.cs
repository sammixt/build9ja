using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace build9ja.infrastructure.Data.Migrations
{
    public partial class ProductStringId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductIdString",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIdString",
                table: "Products");
        }
    }
}
