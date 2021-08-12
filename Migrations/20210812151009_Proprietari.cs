using Microsoft.EntityFrameworkCore.Migrations;

namespace C3xPAWM.Migrations
{
    public partial class Proprietari : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacchi");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Corrieri",
                table: "Corrieri");

            migrationBuilder.RenameTable(
                name: "Corrieri",
                newName: "Corriere");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Negozi",
                newName: "ProprietarioId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Negozi",
                newName: "Proprietario");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Corriere",
                newName: "ProprietarioId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Corriere",
                newName: "Proprietario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Corriere",
                table: "Corriere",
                column: "CorriereId");

            migrationBuilder.CreateIndex(
                name: "IX_Negozi_ProprietarioId",
                table: "Negozi",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Corriere_ProprietarioId",
                table: "Corriere",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Corriere_AspNetUsers_ProprietarioId",
                table: "Corriere",
                column: "ProprietarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Negozi_AspNetUsers_ProprietarioId",
                table: "Negozi",
                column: "ProprietarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corriere_AspNetUsers_ProprietarioId",
                table: "Corriere");

            migrationBuilder.DropForeignKey(
                name: "FK_Negozi_AspNetUsers_ProprietarioId",
                table: "Negozi");

            migrationBuilder.DropIndex(
                name: "IX_Negozi_ProprietarioId",
                table: "Negozi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Corriere",
                table: "Corriere");

            migrationBuilder.DropIndex(
                name: "IX_Corriere_ProprietarioId",
                table: "Corriere");

            migrationBuilder.RenameTable(
                name: "Corriere",
                newName: "Corrieri");

            migrationBuilder.RenameColumn(
                name: "ProprietarioId",
                table: "Negozi",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Proprietario",
                table: "Negozi",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ProprietarioId",
                table: "Corrieri",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Proprietario",
                table: "Corrieri",
                newName: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Corrieri",
                table: "Corrieri",
                column: "CorriereId");

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.UtenteId);
                });

            migrationBuilder.CreateTable(
                name: "Pacchi",
                columns: table => new
                {
                    PaccoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Citta = table.Column<string>(type: "TEXT", nullable: true),
                    CorriereId = table.Column<int>(type: "INTEGER", nullable: false),
                    NegozioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Provincia = table.Column<string>(type: "TEXT", nullable: true),
                    StatoPacco = table.Column<string>(type: "TEXT", nullable: false),
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Via = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacchi", x => x.PaccoId);
                    table.ForeignKey(
                        name: "FK_Pacchi_Corrieri_CorriereId",
                        column: x => x.CorriereId,
                        principalTable: "Corrieri",
                        principalColumn: "CorriereId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacchi_Negozi_NegozioId",
                        column: x => x.NegozioId,
                        principalTable: "Negozi",
                        principalColumn: "NegozioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacchi_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacchi_CorriereId",
                table: "Pacchi",
                column: "CorriereId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacchi_NegozioId",
                table: "Pacchi",
                column: "NegozioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacchi_UtenteId",
                table: "Pacchi",
                column: "UtenteId");
        }
    }
}
