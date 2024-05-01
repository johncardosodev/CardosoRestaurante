namespace CardosoRestaurante.Services.AuthAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } //Utilizador autenticado
        public string Token { get; set; } //Token de autenticação do utilizador com jwt
    }
}