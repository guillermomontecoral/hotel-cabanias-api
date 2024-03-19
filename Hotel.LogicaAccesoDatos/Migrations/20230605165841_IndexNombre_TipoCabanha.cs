using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class IndexNombre_TipoCabanha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_Value",
                table: "TipoCabanhas",
                newName: "Value");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TipoCabanhas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCabanhas_Value",
                table: "TipoCabanhas",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoCabanhas_Value",
                table: "TipoCabanhas");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "TipoCabanhas",
                newName: "Nombre_Value");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre_Value",
                table: "TipoCabanhas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
