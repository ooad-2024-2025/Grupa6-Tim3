using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class Lokacija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lokacija",
                table: "Nekretnina");

            migrationBuilder.CreateIndex(
                name: "IX_Lokacija_nekretninaId",
                table: "Lokacija",
                column: "nekretninaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lokacija_Nekretnina_nekretninaId",
                table: "Lokacija",
                column: "nekretninaId",
                principalTable: "Nekretnina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lokacija_Nekretnina_nekretninaId",
                table: "Lokacija");

            migrationBuilder.DropIndex(
                name: "IX_Lokacija_nekretninaId",
                table: "Lokacija");

            migrationBuilder.AddColumn<string>(
                name: "lokacija",
                table: "Nekretnina",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
