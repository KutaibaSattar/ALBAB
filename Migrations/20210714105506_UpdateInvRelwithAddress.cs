using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class UpdateInvRelwithAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoices_dbaccounts_dbaccountid",
                table: "invoices");

            migrationBuilder.DropIndex(
                name: "ix_invoices_dbaccountid",
                table: "invoices");

            migrationBuilder.AddColumn<int>(
                name: "addressid",
                table: "invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_invoices_addressid",
                table: "invoices",
                column: "addressid");

            migrationBuilder.AddForeignKey(
                name: "fk_invoices_address_addressid",
                table: "invoices",
                column: "addressid",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoices_address_addressid",
                table: "invoices");

            migrationBuilder.DropIndex(
                name: "ix_invoices_addressid",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "addressid",
                table: "invoices");

            migrationBuilder.CreateIndex(
                name: "ix_invoices_dbaccountid",
                table: "invoices",
                column: "dbaccountid");

            migrationBuilder.AddForeignKey(
                name: "fk_invoices_dbaccounts_dbaccountid",
                table: "invoices",
                column: "dbaccountid",
                principalTable: "dbaccounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
