using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoCabanhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoPorHuesped = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCabanhas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabanhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdTipoCabanha = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jacuzzi = table.Column<bool>(type: "bit", nullable: false),
                    HabilitadaParaReservas = table.Column<bool>(type: "bit", nullable: false),
                    NumHabitacion = table.Column<int>(type: "int", nullable: false),
                    CantMaxPersonas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabanhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabanhas_TipoCabanhas_IdTipoCabanha",
                        column: x => x.IdTipoCabanha,
                        principalTable: "TipoCabanhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FotosCabanhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCabanha = table.Column<int>(type: "int", nullable: false),
                    NombreFoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secuenciador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosCabanhas", x => new { x.IdCabanha, x.Id });
                    table.ForeignKey(
                        name: "FK_FotosCabanhas_Cabanhas_IdCabanha",
                        column: x => x.IdCabanha,
                        principalTable: "Cabanhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoMantenimiento = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NomRealizoTrabajo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCabanha = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Cabanhas_IdCabanha",
                        column: x => x.IdCabanha,
                        principalTable: "Cabanhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "INX_NombreCabanha",
                table: "Cabanhas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "INX_NumHabitacion",
                table: "Cabanhas",
                column: "NumHabitacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabanhas_IdTipoCabanha",
                table: "Cabanhas",
                column: "IdTipoCabanha");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_IdCabanha",
                table: "Mantenimientos",
                column: "IdCabanha");

            migrationBuilder.CreateIndex(
                name: "INX_NombreTipoCabanha",
                table: "TipoCabanhas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "INX_EmailUsuario",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FotosCabanhas");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cabanhas");

            migrationBuilder.DropTable(
                name: "TipoCabanhas");
        }
    }
}
