using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class formateofecha20_06_2023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FotosCabanhas_TipoCabanhas_IdCabanha",
                table: "FotosCabanhas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_TipoCabanhas_IdCabanha",
                table: "Mantenimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoCabanhas_TipoCabanhas_IdTipoCabanha",
                table: "TipoCabanhas");

            migrationBuilder.DropIndex(
                name: "INX_NumHabitacion",
                table: "TipoCabanhas");

            migrationBuilder.DropIndex(
                name: "IX_TipoCabanhas_IdTipoCabanha",
                table: "TipoCabanhas");

            migrationBuilder.DropColumn(
                name: "CantMaxPersonas",
                table: "TipoCabanhas");

            migrationBuilder.DropColumn(
                name: "HabilitadaParaReservas",
                table: "TipoCabanhas");

            migrationBuilder.DropColumn(
                name: "IdTipoCabanha",
                table: "TipoCabanhas");

            migrationBuilder.DropColumn(
                name: "Jacuzzi",
                table: "TipoCabanhas");

            migrationBuilder.DropColumn(
                name: "NumHabitacion",
                table: "TipoCabanhas");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Cabanhas",
                newName: "Descripcion_Descripcion");

            migrationBuilder.RenameColumn(
                name: "CabanhaId",
                table: "Cabanhas",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cabanhas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CantMaxPersonas",
                table: "Cabanhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HabilitadaParaReservas",
                table: "Cabanhas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoCabanha",
                table: "Cabanhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Jacuzzi",
                table: "Cabanhas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumHabitacion",
                table: "Cabanhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "INX_NumHabitacion",
                table: "Cabanhas",
                column: "NumHabitacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabanhas_IdTipoCabanha",
                table: "Cabanhas",
                column: "IdTipoCabanha");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabanhas_TipoCabanhas_IdTipoCabanha",
                table: "Cabanhas",
                column: "IdTipoCabanha",
                principalTable: "TipoCabanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FotosCabanhas_Cabanhas_IdCabanha",
                table: "FotosCabanhas",
                column: "IdCabanha",
                principalTable: "Cabanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_Cabanhas_IdCabanha",
                table: "Mantenimientos",
                column: "IdCabanha",
                principalTable: "Cabanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabanhas_TipoCabanhas_IdTipoCabanha",
                table: "Cabanhas");

            migrationBuilder.DropForeignKey(
                name: "FK_FotosCabanhas_Cabanhas_IdCabanha",
                table: "FotosCabanhas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_Cabanhas_IdCabanha",
                table: "Mantenimientos");

            migrationBuilder.DropIndex(
                name: "INX_NumHabitacion",
                table: "Cabanhas");

            migrationBuilder.DropIndex(
                name: "IX_Cabanhas_IdTipoCabanha",
                table: "Cabanhas");

            migrationBuilder.DropColumn(
                name: "CantMaxPersonas",
                table: "Cabanhas");

            migrationBuilder.DropColumn(
                name: "HabilitadaParaReservas",
                table: "Cabanhas");

            migrationBuilder.DropColumn(
                name: "IdTipoCabanha",
                table: "Cabanhas");

            migrationBuilder.DropColumn(
                name: "Jacuzzi",
                table: "Cabanhas");

            migrationBuilder.DropColumn(
                name: "NumHabitacion",
                table: "Cabanhas");

            migrationBuilder.RenameColumn(
                name: "Descripcion_Descripcion",
                table: "Cabanhas",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cabanhas",
                newName: "CabanhaId");

            migrationBuilder.AddColumn<int>(
                name: "CantMaxPersonas",
                table: "TipoCabanhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HabilitadaParaReservas",
                table: "TipoCabanhas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoCabanha",
                table: "TipoCabanhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Jacuzzi",
                table: "TipoCabanhas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumHabitacion",
                table: "TipoCabanhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CabanhaId",
                table: "Cabanhas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "INX_NumHabitacion",
                table: "TipoCabanhas",
                column: "NumHabitacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoCabanhas_IdTipoCabanha",
                table: "TipoCabanhas",
                column: "IdTipoCabanha");

            migrationBuilder.AddForeignKey(
                name: "FK_FotosCabanhas_TipoCabanhas_IdCabanha",
                table: "FotosCabanhas",
                column: "IdCabanha",
                principalTable: "TipoCabanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_TipoCabanhas_IdCabanha",
                table: "Mantenimientos",
                column: "IdCabanha",
                principalTable: "TipoCabanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoCabanhas_TipoCabanhas_IdTipoCabanha",
                table: "TipoCabanhas",
                column: "IdTipoCabanha",
                principalTable: "TipoCabanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
