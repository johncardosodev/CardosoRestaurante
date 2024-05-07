using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardosoRestaurante.Services.CupaoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarCodigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cupoes",
                keyColumn: "CupaoId",
                keyValue: 1,
                columns: new[] { "CupaoCodigo", "ValorMinimo" },
                values: new object[] { "PJ10", 10 });

            migrationBuilder.UpdateData(
                table: "Cupoes",
                keyColumn: "CupaoId",
                keyValue: 2,
                column: "ValorMinimo",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Cupoes",
                keyColumn: "CupaoId",
                keyValue: 3,
                column: "ValorMinimo",
                value: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cupoes",
                keyColumn: "CupaoId",
                keyValue: 1,
                columns: new[] { "CupaoCodigo", "ValorMinimo" },
                values: new object[] { "PJ20", 50 });

            migrationBuilder.UpdateData(
                table: "Cupoes",
                keyColumn: "CupaoId",
                keyValue: 2,
                column: "ValorMinimo",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Cupoes",
                keyColumn: "CupaoId",
                keyValue: 3,
                column: "ValorMinimo",
                value: 150);
        }
    }
}
