using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recuperatorio.Migrations
{
    /// <inheritdoc />
    public partial class SectoresTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorID",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    SectorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.SectorID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_SectorID",
                table: "Empleados",
                column: "SectorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Sectores_SectorID",
                table: "Empleados",
                column: "SectorID",
                principalTable: "Sectores",
                principalColumn: "SectorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Sectores_SectorID",
                table: "Empleados");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_SectorID",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "SectorID",
                table: "Empleados");
        }
    }
}
