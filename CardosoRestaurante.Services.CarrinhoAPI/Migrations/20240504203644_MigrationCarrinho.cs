using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardosoRestaurante.Services.Carrinho.API.Migrations
{
    /// <inheritdoc />
    public partial class MigrationCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoInfos",
                columns: table => new
                {
                    CarrinhoInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CupaoCodigo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoInfos", x => x.CarrinhoInfoId);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoDetalhes",
                columns: table => new
                {
                    CarrinhoDetalhesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoInfoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoDetalhes", x => x.CarrinhoDetalhesId);
                    table.ForeignKey(
                        name: "FK_CarrinhoDetalhes_CarrinhoInfos_CarrinhoInfoId",
                        column: x => x.CarrinhoInfoId,
                        principalTable: "CarrinhoInfos",
                        principalColumn: "CarrinhoInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoDetalhes_CarrinhoInfoId",
                table: "CarrinhoDetalhes",
                column: "CarrinhoInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoDetalhes");

            migrationBuilder.DropTable(
                name: "CarrinhoInfos");
        }
    }
}
