using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C3xPAWM.Migrations
{
    public partial class Pubblicita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataFine",
                table: "Pubblicita",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInizio",
                table: "Pubblicita",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFine",
                table: "Pubblicita");

            migrationBuilder.DropColumn(
                name: "DataInizio",
                table: "Pubblicita");
        }
    }
}
