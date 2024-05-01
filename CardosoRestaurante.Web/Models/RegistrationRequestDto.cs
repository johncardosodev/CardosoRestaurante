namespace CardosoRestaurante.Web.Models.DTO
{
    public class RegistrationRequestDto
    {
        public string Nome { get; set; } //Nome do utilizador
        public string Email { get; set; } //Email do utilizador
        public string Telemovel { get; set; } //Password do utilizador
        public string Password { get; set; } //Password do utilizador

        public string? Funcao { get; set; } //Função do utilizador
    }
}