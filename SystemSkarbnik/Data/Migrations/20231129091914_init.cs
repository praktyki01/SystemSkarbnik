using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSkarbnik.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klasa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Skarbnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imię = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KlasaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skarbnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Skarbnik_Klasa_KlasaID",
                        column: x => x.KlasaID,
                        principalTable: "Klasa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uczen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imię = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KlasaID = table.Column<int>(type: "int", nullable: false),
                    UczenUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczen", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Uczen_AspNetUsers_UczenUserID",
                        column: x => x.UczenUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uczen_Klasa_KlasaID",
                        column: x => x.KlasaID,
                        principalTable: "Klasa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zbiorka",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kwota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KlasaID = table.Column<int>(type: "int", nullable: false),
                    SkarbnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zbiorka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zbiorka_Klasa_KlasaID",
                        column: x => x.KlasaID,
                        principalTable: "Klasa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zbiorka_Skarbnik_SkarbnikID",
                        column: x => x.SkarbnikID,
                        principalTable: "Skarbnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZbiorkaUczen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZbiorkaID = table.Column<int>(type: "int", nullable: false),
                    KlasaID = table.Column<int>(type: "int", nullable: false),
                    UczenID = table.Column<int>(type: "int", nullable: false),
                    CzyZaplacil = table.Column<bool>(type: "bit", nullable: false),
                    KiedyZaplacil = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZbiorkaUczen", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZbiorkaUczen_Klasa_KlasaID",
                        column: x => x.KlasaID,
                        principalTable: "Klasa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZbiorkaUczen_Uczen_UczenID",
                        column: x => x.UczenID,
                        principalTable: "Uczen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZbiorkaUczen_Zbiorka_ZbiorkaID",
                        column: x => x.ZbiorkaID,
                        principalTable: "Zbiorka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skarbnik_KlasaID",
                table: "Skarbnik",
                column: "KlasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uczen_KlasaID",
                table: "Uczen",
                column: "KlasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uczen_UczenUserID",
                table: "Uczen",
                column: "UczenUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Zbiorka_KlasaID",
                table: "Zbiorka",
                column: "KlasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zbiorka_SkarbnikID",
                table: "Zbiorka",
                column: "SkarbnikID");

            migrationBuilder.CreateIndex(
                name: "IX_ZbiorkaUczen_KlasaID",
                table: "ZbiorkaUczen",
                column: "KlasaID");

            migrationBuilder.CreateIndex(
                name: "IX_ZbiorkaUczen_UczenID",
                table: "ZbiorkaUczen",
                column: "UczenID");

            migrationBuilder.CreateIndex(
                name: "IX_ZbiorkaUczen_ZbiorkaID",
                table: "ZbiorkaUczen",
                column: "ZbiorkaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZbiorkaUczen");

            migrationBuilder.DropTable(
                name: "Uczen");

            migrationBuilder.DropTable(
                name: "Zbiorka");

            migrationBuilder.DropTable(
                name: "Skarbnik");

            migrationBuilder.DropTable(
                name: "Klasa");
        }
    }
}
