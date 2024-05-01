namespace CardosoRestaurante.Services.AuthAPI.Models.DTO
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } //Nome do utilizador
        public string Password { get; set; } //Password do utilizador
    }
}