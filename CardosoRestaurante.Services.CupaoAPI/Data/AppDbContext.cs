using CardosoRestaurante.Services.CupaoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CardosoRestaurante.Services.CupaoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //AppDbContext
        {
        }

        public DbSet<Cupao> Cupoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Isto é necessário para evitar erros ao criar a base de dados com a migração do Entity Framework Core

            modelBuilder.Entity<Cupao>().HasData(new Cupao { CupaoId = 1, CupaoCodigo = "PJ20", Desconto = 10, ValorMinimo = 50 });
            modelBuilder.Entity<Cupao>().HasData(new Cupao { CupaoId = 2, CupaoCodigo = "RBOTICAS10", Desconto = 10, ValorMinimo = 100 });
            modelBuilder.Entity<Cupao>().HasData(new Cupao { CupaoId = 3, CupaoCodigo = "MARCOS10", Desconto = 10, ValorMinimo = 150 });
        }
    }
}