using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeleteInPoruka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "primalacId",
                table: "Poruka",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "posiljalacId",
                table: "Poruka",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_posiljalacId",
                table: "Poruka",
                column: "posiljalacId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_primalacId",
                table: "Poruka",
                column: "primalacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poruka_AspNetUsers_posiljalacId",
                table: "Poruka",
                column: "posiljalacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poruka_AspNetUsers_primalacId",
                table: "Poruka",
                column: "primalacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poruka_AspNetUsers_posiljalacId",
                table: "Poruka");

            migrationBuilder.DropForeignKey(
                name: "FK_Poruka_AspNetUsers_primalacId",
                table: "Poruka");

            migrationBuilder.DropIndex(
                name: "IX_Poruka_posiljalacId",
                table: "Poruka");

            migrationBuilder.DropIndex(
                name: "IX_Poruka_primalacId",
                table: "Poruka");

            migrationBuilder.AlterColumn<string>(
                name: "primalacId",
                table: "Poruka",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "posiljalacId",
                table: "Poruka",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
