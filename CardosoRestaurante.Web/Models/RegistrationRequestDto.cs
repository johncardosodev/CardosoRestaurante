using System.ComponentModel.DataAnnotations;

namespace CardosoRestaurante.Web.Models
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } //Nome do utilizador

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; } //Email do utilizador

        [Required(ErrorMessage = "O campo Telemóvel é obrigatório.")]
        public string Telemovel { get; set; } //Password do utilizador

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public string Password { get; set; } //Password do utilizador

        public string? Funcao { get; set; } //Função do utilizador
    }
}