using Microsoft.EntityFrameworkCore.Migrations;

namespace ALBaB.Migrations
{
    public partial class IsExpandableDbAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isexpandable",
                table: "dbaccounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isexpandable",
                table: "dbaccounts");
        }
    }
}
