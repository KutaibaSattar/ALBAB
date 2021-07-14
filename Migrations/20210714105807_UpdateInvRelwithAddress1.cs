using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class UpdateInvRelwithAddress1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoices_aspnetusers_appuserid",
                table: "invoices");

            migrationBuilder.DropIndex(
                name: "ix_invoices_appuserid",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "appuserid",
                table: "invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "appuserid",
                table: "invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_invoices_appuserid",
                table: "invoices",
                column: "appuserid");

            migrationBuilder.AddForeignKey(
                name: "fk_invoices_aspnetusers_appuserid",
                table: "invoices",
                column: "appuserid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
