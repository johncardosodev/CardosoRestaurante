using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardosoRestaurante.Services.ProdutoAPI.Migrations
{
    /// <inheritdoc />
    public partial class novosprodutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Porcao = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ProdutoId", "Categoria", "Descricao", "ImagemUrl", "Nome", "Porcao", "Preco" },
                values: new object[,]
                {
                    { 1, "Entradas", "Salada de alface, tomate, cenoura, pepino, cebola, ovo cozido, atum, milho, azeitonas e molho de iogurte.", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/mikado-salad.jpg/thumb", "Salada Mikado", 1, 7.9900000000000002 },
                    { 2, "Entradas", "Mini crepes estaladiços recheados de legumes variados, acompanhado de molho agridoce.", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/mini-vegetable-crepes.jpg/thumb", "Mini Crepes de Legumes", 6, 5.0 },
                    { 3, "Entradas", "Camarões panados em panko com molho agridoce", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/fried-shrimp.jpg/details", "Camarão Panado", 6, 6.5 },
                    { 4, "Sopas", "Sopa de legumes variados.", "https://j6t2y8j5.rocketcdn.me/wp-content/uploads/2021/02/como-fazer-sopa-de-legumes-historia-do-prato-receitas-deliciosas.jpg", "Sopa de Legumes", 1, 3.5 },
                    { 5, "Sopas", "Sopa de peixe com peixe variado.", "https://www.becel.com/pt-pt/-/media/Project/Upfield/Brands/Becel-NL/Becel-PT/Assets/Recipes/94adcfb4-e9b6-4738-8037-76d905e8a40c.jpg?rev=db87e9aadfb74e87a12dfec4acc944b6", "Sopa de Peixe", 1, 4.5 },
                    { 10, "Sushi", "Sushi em forma de cone para se comer à mão recheado de salmão, abacate, manga e queijo creme.", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/salmon-temaki.jpg/details", "Temaki de Salmão", 1, 6.0 },
                    { 11, "Sushi", "Sushi em forma de cone para se comer à mão recheado de camarão panado, queijo creme e molho teriyaki.", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/crispy-ebi-temaki.jpg/details", "Temaki Camarão Crocante", 1, 6.0 },
                    { 12, "Sushi", "Rolo de sushi invertido panado em panko, recheado com pasta de peixe e maionese, com molho teriyaki.", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/hot-uramaki.jpg/details", "Hot Uramaki", 4, 3.5 },
                    { 13, "Sushi", "Variedade de legumes e cogumelos salteados na wok.", "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/sauteed-vegetables.jpg/details", "Legumes Salteados", 1, 4.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
