using Microsoft.EntityFrameworkCore.Migrations;

namespace C3xPAWM.Migrations
{
    public partial class StatoPacco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatoPacco",
                table: "Pacco",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatoPacco",
                table: "Pacco");
        }
    }
}
