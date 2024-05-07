using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace build9ja.infrastructure.Data.Migrations
{
    public partial class RedisTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateCreate",
                table: "Permissions",
                newName: "DateCreated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Vendors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "VendorCredentials",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "vendorBankInfos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Staffs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "StaffEventLogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "StaffCredentials",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProductVariations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProductSpecifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProductImages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Banner",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Redis",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Expiry = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Redis");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "VendorCredentials");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "vendorBankInfos");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "StaffEventLogs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "StaffCredentials");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProductVariations");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProductSpecifications");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Banner");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Permissions",
                newName: "dateCreate");
        }
    }
}
