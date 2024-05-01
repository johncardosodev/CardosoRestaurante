using CardosoRestaurante.Web.Models;

namespace CardosoRestaurante.Web.Service.IService
{
    public interface IBaseService
    {
        // interface IBaseService possui um único método chamado EnviarAsync, que recebe um objeto do tipo RequestDto e retorna um objeto do tipo ResponseDto.
        /// <summary>
        /// Envia uma solicitação HTTP para o URI especificado.
        /// </summary>
        /// <param name="request">O objeto RequestDto contendo os dados da solicitação.</param>
        /// <returns>O objeto ResponseDto contendo a resposta da solicitação.</returns>
        Task<ResponseDto?> EnviarAsync(RequestDto request); // Envia uma solicitação HTTP para o URI especificado

        // interface IBaseService é fornecer um contrato comum para as classes que a implementam, permitindo a flexibilidade, modularidade e aplicação de princípios de programação orientada a objetos.    }
    }
}