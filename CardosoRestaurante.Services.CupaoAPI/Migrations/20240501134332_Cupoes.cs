using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardosoRestaurante.Services.CupaoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cupoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupoes",
                columns: table => new
                {
                    CupaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CupaoCodigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desconto = table.Column<double>(type: "float", nullable: false),
                    ValorMinimo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupoes", x => x.CupaoId);
                });

            migrationBuilder.InsertData(
                table: "Cupoes",
                columns: new[] { "CupaoId", "CupaoCodigo", "Desconto", "ValorMinimo" },
                values: new object[,]
                {
                    { 1, "PJ20", 10.0, 50 },
                    { 2, "RBOTICAS10", 10.0, 100 },
                    { 3, "MARCOS10", 10.0, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupoes");
        }
    }
}
