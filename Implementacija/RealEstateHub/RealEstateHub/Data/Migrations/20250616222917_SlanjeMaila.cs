using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class SlanjeMaila : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ZeliObavjestenja",
                table: "FilterNekretnina",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZeliObavjestenja",
                table: "FilterNekretnina");
        }
    }
}
