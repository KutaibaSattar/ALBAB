using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class UpdateAddress2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_address",
                table: "address");

            migrationBuilder.AddPrimaryKey(
                name: "pk_address",
                table: "address",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_address_appuserid",
                table: "address",
                column: "appuserid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_address",
                table: "address");

            migrationBuilder.DropIndex(
                name: "ix_address_appuserid",
                table: "address");

            migrationBuilder.AddPrimaryKey(
                name: "pk_address",
                table: "address",
                columns: new[] { "appuserid", "id" });
        }
    }
}
