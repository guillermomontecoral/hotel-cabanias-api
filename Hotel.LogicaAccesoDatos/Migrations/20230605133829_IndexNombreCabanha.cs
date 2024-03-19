using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class IndexNombreCabanha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_Nombre",
                table: "Cabanhas",
                newName: "Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Cabanhas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cabanhas_Nombre",
                table: "Cabanhas",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabanhas_Nombre",
                table: "Cabanhas");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Cabanhas",
                newName: "Nombre_Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre_Nombre",
                table: "Cabanhas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
