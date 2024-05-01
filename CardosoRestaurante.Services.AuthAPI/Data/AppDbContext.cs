using CardosoRestaurante.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardosoRestaurante.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> //Deixou de ser IdentityUser e passou a ser ApplicationUser. Herda da IdentityUser para podermos adicionar o nome ao utilizador
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } //Adicionar a tabela de utilizadores com o nome

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}