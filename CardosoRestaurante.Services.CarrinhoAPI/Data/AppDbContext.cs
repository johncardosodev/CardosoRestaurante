using CardosoRestaurante.Services.CarrinhoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CardosoRestaurante.Services.Carrinho.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //AppDbContext
        {
        }

        public DbSet<CarrinhoInfo> CarrinhoInfos { get; set; }
        public DbSet<CarrinhoDetalhes> CarrinhoDetalhes { get; set; }
    }
}