using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracijaFilterNekretina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "kvadratura",
                table: "FilterNekretnina",
                newName: "minKvadratura");

            migrationBuilder.RenameColumn(
                name: "brojSoba",
                table: "FilterNekretnina",
                newName: "minBrojSoba");

            migrationBuilder.AddColumn<int>(
                name: "maxBrojSoba",
                table: "FilterNekretnina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "maxKvadratura",
                table: "FilterNekretnina",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxBrojSoba",
                table: "FilterNekretnina");

            migrationBuilder.DropColumn(
                name: "maxKvadratura",
                table: "FilterNekretnina");

            migrationBuilder.RenameColumn(
                name: "minKvadratura",
                table: "FilterNekretnina",
                newName: "kvadratura");

            migrationBuilder.RenameColumn(
                name: "minBrojSoba",
                table: "FilterNekretnina",
                newName: "brojSoba");
        }
    }
}
