using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ALBaB.Migrations
{
    public partial class Purchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchhdrs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    purno = table.Column<string>(type: "text", nullable: true),
                    purdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    purcomment = table.Column<string>(type: "text", nullable: true),
                    lastupdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    appuserid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchhdrs", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchhdrs_aspnetusers_appuserid",
                        column: x => x.appuserid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchdtls",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    lastupdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    purchhdrid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchdtls", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchdtls_products_productid",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_purchdtls_purchhdrs_purchhdrid",
                        column: x => x.purchhdrid,
                        principalTable: "purchhdrs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_purchdtls_productid",
                table: "purchdtls",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_purchdtls_purchhdrid",
                table: "purchdtls",
                column: "purchhdrid");

            migrationBuilder.CreateIndex(
                name: "ix_purchhdrs_appuserid",
                table: "purchhdrs",
                column: "appuserid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchdtls");

            migrationBuilder.DropTable(
                name: "purchhdrs");
        }
    }
}
