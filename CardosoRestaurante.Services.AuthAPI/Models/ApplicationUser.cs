using Microsoft.AspNetCore.Identity;

namespace CardosoRestaurante.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } //Adicionar nome ao utilizador
    }
}