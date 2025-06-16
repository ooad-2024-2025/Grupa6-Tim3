using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class SlanjeMaila2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatumDodavanja",
                table: "Nekretnina",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumDodavanja",
                table: "Nekretnina");
        }
    }
}
