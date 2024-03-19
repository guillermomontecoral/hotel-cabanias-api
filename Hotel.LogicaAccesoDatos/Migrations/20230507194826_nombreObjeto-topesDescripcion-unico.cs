using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class nombreObjetotopesDescripcionunico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreObj",
                table: "TopesDescripciones",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "INX_NombreObjeto",
                table: "TopesDescripciones",
                column: "NombreObj",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "INX_NombreObjeto",
                table: "TopesDescripciones");

            migrationBuilder.AlterColumn<string>(
                name: "NombreObj",
                table: "TopesDescripciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
