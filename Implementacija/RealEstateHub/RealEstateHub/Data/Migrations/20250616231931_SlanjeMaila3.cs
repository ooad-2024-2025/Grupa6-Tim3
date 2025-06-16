using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class SlanjeMaila3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoslanaObavjestenja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NekretninaId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DatumSlanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoslanaObavjestenja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoslanaObavjestenja_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoslanaObavjestenja_Nekretnina_NekretninaId",
                        column: x => x.NekretninaId,
                        principalTable: "Nekretnina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoslanaObavjestenja_KorisnikId",
                table: "PoslanaObavjestenja",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PoslanaObavjestenja_NekretninaId",
                table: "PoslanaObavjestenja",
                column: "NekretninaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoslanaObavjestenja");
        }
    }
}
