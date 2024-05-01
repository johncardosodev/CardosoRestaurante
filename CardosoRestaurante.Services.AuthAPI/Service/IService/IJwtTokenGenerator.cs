using CardosoRestaurante.Services.AuthAPI.Models;

namespace CardosoRestaurante.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Este método recebe as informações do usuário, cria um token JWT com base nessas informações e retorna o token gerado como uma string. Esse token pode ser usado para autenticar e autorizar o usuário em uma aplicação.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns>String token</returns>
        public string GenerateToken(ApplicationUser applicationUser);
    }
}