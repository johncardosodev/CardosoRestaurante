using System.ComponentModel.DataAnnotations;

namespace CardosoRestaurante.Web.Models
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "O campo Nome de utilizador é obrigatório.")]
        public string UserName { get; set; } //Nome do utilizador

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public string Password { get; set; } //Password do utilizador
    }
}