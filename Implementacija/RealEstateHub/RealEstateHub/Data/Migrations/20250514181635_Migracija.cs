using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    korisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imeKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prezimeKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lozinkaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumPridruzivanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.korisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik_Nekretnina",
                columns: table => new
                {
                    kn_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nekretninaId = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik_Nekretnina", x => x.kn_id);
                });

            migrationBuilder.CreateTable(
                name: "Lokacija",
                columns: table => new
                {
                    lokacijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nekretninaId = table.Column<int>(type: "int", nullable: false),
                    grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latituda = table.Column<double>(type: "float", nullable: false),
                    longituda = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacija", x => x.lokacijaId);
                });

            migrationBuilder.CreateTable(
                name: "Nekretnina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opisNekretnine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cijena = table.Column<double>(type: "float", nullable: false),
                    kvadratura = table.Column<double>(type: "float", nullable: false),
                    lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojSoba = table.Column<int>(type: "int", nullable: false),
                    vlasnikId = table.Column<int>(type: "int", nullable: false),
                    vrstaNekretnine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nekretnina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obavjestenje",
                columns: table => new
                {
                    obavjestenjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikId = table.Column<int>(type: "int", nullable: false),
                    datumObavjestenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    poruka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavjestenje", x => x.obavjestenjeId);
                });

            migrationBuilder.CreateTable(
                name: "Poruka",
                columns: table => new
                {
                    porukaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    posiljalacId = table.Column<int>(type: "int", nullable: false),
                    primalacId = table.Column<int>(type: "int", nullable: false),
                    sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    procitano = table.Column<bool>(type: "bit", nullable: false),
                    datumSlanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruka", x => x.porukaId);
                });

            migrationBuilder.CreateTable(
                name: "Vlasnik_Nekretnina",
                columns: table => new
                {
                    vn_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nekretninaId = table.Column<int>(type: "int", nullable: false),
                    vlasnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlasnik_Nekretnina", x => x.vn_id);
                });

            migrationBuilder.CreateTable(
                name: "Sesija",
                columns: table => new
                {
                    sesijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prijavljeniKorisnikkorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesija", x => x.sesijaId);
                    table.ForeignKey(
                        name: "FK_Sesija_Korisnik_prijavljeniKorisnikkorisnikId",
                        column: x => x.prijavljeniKorisnikkorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "korisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterNekretnina",
                columns: table => new
                {
                    filterNekretninaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    minCijena = table.Column<double>(type: "float", nullable: false),
                    maxCijena = table.Column<double>(type: "float", nullable: false),
                    brojSoba = table.Column<int>(type: "int", nullable: false),
                    kvadratura = table.Column<int>(type: "int", nullable: false),
                    tipNekretnine = table.Column<int>(type: "int", nullable: false),
                    lokacijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterNekretnina", x => x.filterNekretninaId);
                    table.ForeignKey(
                        name: "FK_FilterNekretnina_Lokacija_lokacijaId",
                        column: x => x.lokacijaId,
                        principalTable: "Lokacija",
                        principalColumn: "lokacijaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    oglasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jeAktivan = table.Column<bool>(type: "bit", nullable: false),
                    nekretninaId = table.Column<int>(type: "int", nullable: false),
                    datumPostavljanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    brojPregleda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.oglasId);
                    table.ForeignKey(
                        name: "FK_Oglas_Nekretnina_nekretninaId",
                        column: x => x.nekretninaId,
                        principalTable: "Nekretnina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kriterij",
                columns: table => new
                {
                    kriterijId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    obavjestenjeId = table.Column<int>(type: "int", nullable: false),
                    vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kriterij", x => x.kriterijId);
                    table.ForeignKey(
                        name: "FK_Kriterij_Obavjestenje_obavjestenjeId",
                        column: x => x.obavjestenjeId,
                        principalTable: "Obavjestenje",
                        principalColumn: "obavjestenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterNekretnina_lokacijaId",
                table: "FilterNekretnina",
                column: "lokacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kriterij_obavjestenjeId",
                table: "Kriterij",
                column: "obavjestenjeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_nekretninaId",
                table: "Oglas",
                column: "nekretninaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesija_prijavljeniKorisnikkorisnikId",
                table: "Sesija",
                column: "prijavljeniKorisnikkorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterNekretnina");

            migrationBuilder.DropTable(
                name: "Korisnik_Nekretnina");

            migrationBuilder.DropTable(
                name: "Kriterij");

            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "Poruka");

            migrationBuilder.DropTable(
                name: "Sesija");

            migrationBuilder.DropTable(
                name: "Vlasnik_Nekretnina");

            migrationBuilder.DropTable(
                name: "Lokacija");

            migrationBuilder.DropTable(
                name: "Obavjestenje");

            migrationBuilder.DropTable(
                name: "Nekretnina");

            migrationBuilder.DropTable(
                name: "Korisnik");
        }
    }
}
