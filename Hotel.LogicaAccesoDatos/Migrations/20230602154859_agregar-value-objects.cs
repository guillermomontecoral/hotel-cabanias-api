using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class agregarvalueobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "INX_NombreObjeto",
                table: "TopesDescripciones");

            migrationBuilder.DropIndex(
                name: "INX_NombreTipoCabanha",
                table: "TipoCabanhas");

            migrationBuilder.DropIndex(
                name: "INX_NombreCabanha",
                table: "Cabanhas");

            migrationBuilder.DropColumn(
                name: "NombreObj",
                table: "TopesDescripciones");

            migrationBuilder.RenameColumn(
                name: "TopeMin",
                table: "TopesDescripciones",
                newName: "Rangos_Min");

            migrationBuilder.RenameColumn(
                name: "TopeMax",
                table: "TopesDescripciones",
                newName: "Rangos_Max");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "TipoCabanhas",
                newName: "Nombre_Nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "TipoCabanhas",
                newName: "Descripcion_Descripcion");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Mantenimientos",
                newName: "Fecha_Fecha");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Mantenimientos",
                newName: "Descripcion_Descripcion");

            migrationBuilder.RenameColumn(
                name: "NomRealizoTrabajo",
                table: "Mantenimientos",
                newName: "RealizadoPor_Nombre");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Cabanhas",
                newName: "Nombre_Nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Cabanhas",
                newName: "Descripcion_Descripcion");

            migrationBuilder.AddColumn<string>(
                name: "NombreTope_Nombre",
                table: "TopesDescripciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre_Nombre",
                table: "TipoCabanhas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre_Nombre",
                table: "Cabanhas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreTope_Nombre",
                table: "TopesDescripciones");

            migrationBuilder.RenameColumn(
                name: "Rangos_Min",
                table: "TopesDescripciones",
                newName: "TopeMin");

            migrationBuilder.RenameColumn(
                name: "Rangos_Max",
                table: "TopesDescripciones",
                newName: "TopeMax");

            migrationBuilder.RenameColumn(
                name: "Nombre_Nombre",
                table: "TipoCabanhas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion_Descripcion",
                table: "TipoCabanhas",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Fecha_Fecha",
                table: "Mantenimientos",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "Descripcion_Descripcion",
                table: "Mantenimientos",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "RealizadoPor_Nombre",
                table: "Mantenimientos",
                newName: "NomRealizoTrabajo");

            migrationBuilder.RenameColumn(
                name: "Nombre_Nombre",
                table: "Cabanhas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion_Descripcion",
                table: "Cabanhas",
                newName: "Descripcion");

            migrationBuilder.AddColumn<string>(
                name: "NombreObj",
                table: "TopesDescripciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoCabanhas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Cabanhas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "INX_NombreObjeto",
                table: "TopesDescripciones",
                column: "NombreObj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "INX_NombreTipoCabanha",
                table: "TipoCabanhas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "INX_NombreCabanha",
                table: "Cabanhas",
                column: "Nombre",
                unique: true);
        }
    }
}
