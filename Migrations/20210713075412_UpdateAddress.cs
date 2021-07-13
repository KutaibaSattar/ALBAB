using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ALBaB.Migrations
{
    public partial class UpdateAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_aspnetusers_appuserid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_ordermethod_ordermethodid",
                table: "orders");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "ordermethod");

            migrationBuilder.DropPrimaryKey(
                name: "pk_orders",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_appuserid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_ordermethodid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Address_appuserid",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "city",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "country",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "line1",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "line2",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "region",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "orderdate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ordermethodid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "paymentintentid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "subtotal",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "address");

            migrationBuilder.AddPrimaryKey(
                name: "pk_address",
                table: "address",
                columns: new[] { "appuserid", "id" });

            migrationBuilder.AddForeignKey(
                name: "fk_address_aspnetusers_appuserid",
                table: "address",
                column: "appuserid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_aspnetusers_appuserid",
                table: "address");

            migrationBuilder.DropPrimaryKey(
                name: "pk_address",
                table: "address");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "orders");

            migrationBuilder.AddColumn<int>(
                name: "Address_appuserid",
                table: "aspnetusers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "aspnetusers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "aspnetusers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "line1",
                table: "aspnetusers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "line2",
                table: "aspnetusers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "region",
                table: "aspnetusers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "orderdate",
                table: "orders",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "ordermethodid",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "paymentintentid",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "orders",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "subtotal",
                table: "orders",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "pk_orders",
                table: "orders",
                column: "id");

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    orderid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_orderitems_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordermethod",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    ordertime = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    shortname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ordermethod", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_appuserid",
                table: "orders",
                column: "appuserid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_ordermethodid",
                table: "orders",
                column: "ordermethodid");

            migrationBuilder.CreateIndex(
                name: "ix_orderitems_orderid",
                table: "orderitems",
                column: "orderid");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_aspnetusers_appuserid",
                table: "orders",
                column: "appuserid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_ordermethod_ordermethodid",
                table: "orders",
                column: "ordermethodid",
                principalTable: "ordermethod",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
