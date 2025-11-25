using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventosShared.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerInscricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelada",
                table: "Inscricoes");

            migrationBuilder.AddColumn<int>(
                name: "VagasUtilizadas",
                table: "Eventos",
                type: "int",
                nullable: false,
                defaultValue: 0);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VagasUtilizadas",
                table: "Eventos");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelada",
                table: "Inscricoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
		}
    }
}
