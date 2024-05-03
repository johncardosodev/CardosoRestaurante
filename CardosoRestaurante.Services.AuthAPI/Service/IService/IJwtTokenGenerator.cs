using CardosoRestaurante.Services.AuthAPI.Models;

namespace CardosoRestaurante.Services.AuthAPI.Service.IService
{
    /// <summary>
    /// Esta interface serve como um contrato que define o comportamento de uma classe geradora de tokens, garantindo que qualquer classe que implemente esta interface terá um método GenerateToken que gera um token JWT com base nas informações do usuário fornecidas.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Este método recebe as informações do usuário, cria um token JWT com base nessas informações e retorna o token gerado como uma string. Esse token pode ser usado para autenticar e autorizar o usuário em uma aplicação.
        /// </summary>
        /// <param name="applicationUser">The user information.</param>
        /// <returns>The generated token as a string.</returns>
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}