using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recuperatorio.Migrations
{
    /// <inheritdoc />
    public partial class informeTablasDos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Localidades_LocalidadID",
                table: "Empleados");

            migrationBuilder.AlterColumn<int>(
                name: "LocalidadID",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Localidades_LocalidadID",
                table: "Empleados",
                column: "LocalidadID",
                principalTable: "Localidades",
                principalColumn: "LocalidadID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Localidades_LocalidadID",
                table: "Empleados");

            migrationBuilder.AlterColumn<int>(
                name: "LocalidadID",
                table: "Empleados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Localidades_LocalidadID",
                table: "Empleados",
                column: "LocalidadID",
                principalTable: "Localidades",
                principalColumn: "LocalidadID");
        }
    }
}
