using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace build9ja.infrastructure.Data.Migrations
{
    public partial class CommissionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_staffEventLogs",
                table: "staffEventLogs");

            migrationBuilder.RenameTable(
                name: "staffEventLogs",
                newName: "StaffEventLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffEventLogs",
                table: "StaffEventLogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommissionType = table.Column<string>(type: "text", nullable: false),
                    CommissionPercentage = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffEventLogs",
                table: "StaffEventLogs");

            migrationBuilder.RenameTable(
                name: "StaffEventLogs",
                newName: "staffEventLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_staffEventLogs",
                table: "staffEventLogs",
                column: "Id");
        }
    }
}
