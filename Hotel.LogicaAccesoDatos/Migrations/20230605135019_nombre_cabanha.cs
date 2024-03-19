using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class nombre_cabanha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_Nombre",
                table: "TipoCabanhas",
                newName: "Nombre_Value");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Cabanhas",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_Cabanhas_Nombre",
                table: "Cabanhas",
                newName: "IX_Cabanhas_Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_Value",
                table: "TipoCabanhas",
                newName: "Nombre_Nombre");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Cabanhas",
                newName: "Nombre");

            migrationBuilder.RenameIndex(
                name: "IX_Cabanhas_Value",
                table: "Cabanhas",
                newName: "IX_Cabanhas_Nombre");
        }
    }
}
