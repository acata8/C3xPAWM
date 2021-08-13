using Microsoft.EntityFrameworkCore.Migrations;

namespace C3xPAWM.Migrations
{
    public partial class Pacco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacco",
                columns: table => new
                {
                    PaccoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Destinazione = table.Column<string>(type: "TEXT", nullable: true),
                    Partenza = table.Column<string>(type: "TEXT", nullable: true),
                    NegozioId = table.Column<int>(type: "INTEGER", nullable: false),
                    CorriereId = table.Column<int>(type: "INTEGER", nullable: false),
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    UtenteId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacco", x => x.PaccoId);
                    table.ForeignKey(
                        name: "FK_Pacco_AspNetUsers_UtenteId1",
                        column: x => x.UtenteId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pacco_Corriere_CorriereId",
                        column: x => x.CorriereId,
                        principalTable: "Corriere",
                        principalColumn: "CorriereId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacco_Negozi_NegozioId",
                        column: x => x.NegozioId,
                        principalTable: "Negozi",
                        principalColumn: "NegozioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacco_CorriereId",
                table: "Pacco",
                column: "CorriereId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacco_NegozioId",
                table: "Pacco",
                column: "NegozioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacco_UtenteId1",
                table: "Pacco",
                column: "UtenteId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacco");
        }
    }
}
