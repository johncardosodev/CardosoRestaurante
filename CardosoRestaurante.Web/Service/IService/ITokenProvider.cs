namespace CardosoRestaurante.Web.Service.IService
{
    /// <summary>
    /// Interface responsável por fornecer e gerenciar o token de autenticação.
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Define o token de autenticação.
        /// </summary>
        /// <param name="token">O token de autenticação.</param>
        void SetToken(string token);

        /// <summary>
        /// Obtém o token de autenticação atual.
        /// </summary>
        /// <returns>O token de autenticação atual.</returns>
        string? GetToken();

        /// <summary>
        /// Limpa o token de autenticação.
        /// </summary>
        void ClearToken();
    }
}