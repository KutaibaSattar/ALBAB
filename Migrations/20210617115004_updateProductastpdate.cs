using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class updateProductastpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "products",
                type: "decimal(7, 2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastupdate",
                table: "products",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastupdate",
                table: "products");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "products",
                type: "decimal(7, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");
        }
    }
}
