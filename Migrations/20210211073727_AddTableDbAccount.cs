using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class AddTableDbAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "dbAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    lvl = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbAccounts_dbAccounts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "dbAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbAccounts_ParentId",
                table: "dbAccounts",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbAccounts");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "AspNetUsers");
        }
    }
}
