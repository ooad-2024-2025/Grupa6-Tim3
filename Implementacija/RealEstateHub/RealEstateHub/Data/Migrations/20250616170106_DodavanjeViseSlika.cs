using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodavanjeViseSlika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Nekretnina");

            migrationBuilder.CreateTable(
                name: "SlikeNekretnine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NekretninaId = table.Column<int>(type: "int", nullable: false),
                    Putanja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlikeNekretnine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlikeNekretnine_Nekretnina_NekretninaId",
                        column: x => x.NekretninaId,
                        principalTable: "Nekretnina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlikeNekretnine_NekretninaId",
                table: "SlikeNekretnine",
                column: "NekretninaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlikeNekretnine");

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Nekretnina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
