using Microsoft.EntityFrameworkCore.Migrations;

namespace C3xPAWM.Migrations
{
    public partial class Pacco2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacco_AspNetUsers_UtenteId1",
                table: "Pacco");

            migrationBuilder.DropIndex(
                name: "IX_Pacco_UtenteId1",
                table: "Pacco");

            migrationBuilder.DropColumn(
                name: "UtenteId1",
                table: "Pacco");

            migrationBuilder.AlterColumn<string>(
                name: "UtenteId",
                table: "Pacco",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Pacco_UtenteId",
                table: "Pacco",
                column: "UtenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacco_AspNetUsers_UtenteId",
                table: "Pacco",
                column: "UtenteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacco_AspNetUsers_UtenteId",
                table: "Pacco");

            migrationBuilder.DropIndex(
                name: "IX_Pacco_UtenteId",
                table: "Pacco");

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "Pacco",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtenteId1",
                table: "Pacco",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacco_UtenteId1",
                table: "Pacco",
                column: "UtenteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacco_AspNetUsers_UtenteId1",
                table: "Pacco",
                column: "UtenteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
