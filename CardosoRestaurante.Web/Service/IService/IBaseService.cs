using CardosoRestaurante.Web.Models;

namespace CardosoRestaurante.Web.Service.IService
{
    /// <summary>
    /// Interface responsável por fornecer um contrato comum para as classes que a implementam, permitindo a flexibilidade, modularidade e aplicação de princípios de programação orientada a objetos.
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Envia uma solicitação HTTP para o URI especificado.
        /// </summary>
        /// <param name="request">O objeto RequestDto contendo os dados da solicitação.</param>
        /// <param name="comBearer">Indica se deve incluir o token de autenticação Bearer na solicitação. O valor padrão é true.</param>
        /// <returns>O objeto ResponseDto contendo a resposta da solicitação.</returns>
        Task<ResponseDto?> EnviarAsync(RequestDto request, bool comBearer = true);
    }
}