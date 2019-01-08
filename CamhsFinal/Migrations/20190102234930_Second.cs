using Microsoft.EntityFrameworkCore.Migrations;

namespace CamhsFinal.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isOpen",
                table: "Episodes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOpen",
                table: "Episodes");
        }
    }
}
