using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoices_aspnetusers_appuserid",
                table: "invoices");

            migrationBuilder.AlterColumn<int>(
                name: "appuserid",
                table: "invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_invoices_aspnetusers_appuserid",
                table: "invoices",
                column: "appuserid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoices_aspnetusers_appuserid",
                table: "invoices");

            migrationBuilder.AlterColumn<int>(
                name: "appuserid",
                table: "invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_invoices_aspnetusers_appuserid",
                table: "invoices",
                column: "appuserid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
