using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "debitaccountid",
                table: "invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "discount",
                table: "invoices",
                type: "decimal(5, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "subtotal",
                table: "invoices",
                type: "decimal(5, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "totalamount",
                table: "invoices",
                type: "decimal(5, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "vat",
                table: "invoices",
                type: "decimal(5, 2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vataccountid",
                table: "invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "debitaccountid",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "subtotal",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "totalamount",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "vat",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "vataccountid",
                table: "invoices");
        }
    }
}
