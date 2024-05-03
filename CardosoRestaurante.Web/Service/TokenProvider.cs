using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

namespace CardosoRestaurante.Web.Service
{
    /// <summary>
    /// Provedor de token responsável por gerenciar o token de autenticação.
    /// </summary>
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="TokenProvider"/>.
        /// </summary>
        /// <param name="httpContextAccessor">O acessor do contexto HTTP.</param>
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Limpa o token de autenticação.
        /// </summary>
        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie); //Apaga o cookie do token de autenticação do navegador do usuário
        }

        /// <summary>
        /// Obtém o token de autenticação.
        /// </summary>
        /// <returns>O token de autenticação.</returns>
        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token); //Tenta obter o token de autenticação do cookie do navegador do usuário e armazena o resultado em hasToken e o token em token se for bem sucedido

            return hasToken == true ? token : null;
        }

        /// <summary>
        /// Define o token de autenticação.
        /// </summary>
        /// <param name="token">O token de autenticação.</param>
        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token); //Define o token de autenticação no cookie do navegador do usuário para autenticar o usuário em solicitações futuras ao servidor da web 
        }
    }
}