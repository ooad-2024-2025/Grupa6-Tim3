using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLokacijaIdFromFilterNekretnina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterNekretnina_Lokacija_lokacijaId",
                table: "FilterNekretnina");

            migrationBuilder.DropIndex(
                name: "IX_FilterNekretnina_lokacijaId",
                table: "FilterNekretnina");

            migrationBuilder.DropColumn(
                name: "lokacijaId",
                table: "FilterNekretnina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "lokacijaId",
                table: "FilterNekretnina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FilterNekretnina_lokacijaId",
                table: "FilterNekretnina",
                column: "lokacijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterNekretnina_Lokacija_lokacijaId",
                table: "FilterNekretnina",
                column: "lokacijaId",
                principalTable: "Lokacija",
                principalColumn: "lokacijaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
