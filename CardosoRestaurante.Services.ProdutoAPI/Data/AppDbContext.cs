using CardosoRestaurante.Services.ProdutoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CardosoRestaurante.Services.ProdutoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //AppDbContext
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Isto é necessário para evitar erros ao criar a base de dados com a migração do Entity Framework Core

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 1,
                Nome = "Salada Mikado",
                Preco = 7.99,
                Descricao = "Salada de alface, tomate, cenoura, pepino, cebola, ovo cozido, atum, milho, azeitonas e molho de iogurte.",
                Categoria = "Entradas",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/mikado-salad.jpg/thumb",
                Porcao = 1,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 2,
                Nome = "Mini Crepes de Legumes",
                Preco = 5,
                Descricao = "Mini crepes estaladiços recheados de legumes variados, acompanhado de molho agridoce.",
                Categoria = "Entradas",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/mini-vegetable-crepes.jpg/thumb",
                Porcao = 6,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 3,
                Nome = "Camarão Panado",
                Preco = 6.5,
                Descricao = "Camarões panados em panko com molho agridoce",
                Categoria = "Entradas",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/fried-shrimp.jpg/details",
                Porcao = 6,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 4,
                Nome = "Sopa de Legumes",
                Preco = 3.5,
                Descricao = "Sopa de legumes variados.",
                Categoria = "Sopas",
                ImagemUrl = "https://j6t2y8j5.rocketcdn.me/wp-content/uploads/2021/02/como-fazer-sopa-de-legumes-historia-do-prato-receitas-deliciosas.jpg",
                Porcao = 1,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 5,
                Nome = "Sopa de Peixe",
                Preco = 4.5,
                Descricao = "Sopa de peixe com peixe variado.",
                Categoria = "Sopas",
                ImagemUrl = "https://www.becel.com/pt-pt/-/media/Project/Upfield/Brands/Becel-NL/Becel-PT/Assets/Recipes/94adcfb4-e9b6-4738-8037-76d905e8a40c.jpg?rev=db87e9aadfb74e87a12dfec4acc944b6",
                Porcao = 1,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 10,
                Nome = "Temaki de Salmão",
                Preco = 6,
                Descricao = "Sushi em forma de cone para se comer à mão recheado de salmão, abacate, manga e queijo creme.",
                Categoria = "Sushi",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/salmon-temaki.jpg/details",
                Porcao = 1,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 11,
                Nome = "Temaki Camarão Crocante",
                Preco = 6,
                Descricao = "Sushi em forma de cone para se comer à mão recheado de camarão panado, queijo creme e molho teriyaki.",
                Categoria = "Sushi",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/crispy-ebi-temaki.jpg/details",
                Porcao = 1,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 12,
                Nome = "Hot Uramaki",
                Preco = 3.50,
                Descricao = "Rolo de sushi invertido panado em panko, recheado com pasta de peixe e maionese, com molho teriyaki.",
                Categoria = "Sushi",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/hot-uramaki.jpg/details",
                Porcao = 4,
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                ProdutoId = 13,
                Nome = "Legumes Salteados",
                Preco = 4.50,
                Descricao = "Variedade de legumes e cogumelos salteados na wok.",
                Categoria = "Sushi",
                ImagemUrl = "https://imagedelivery.net/DDZ73aQyRCLNE8HUNFSEug/businesses/pt510587070/items/sauteed-vegetables.jpg/details",
                Porcao = 1,
            });
        }
    }
}