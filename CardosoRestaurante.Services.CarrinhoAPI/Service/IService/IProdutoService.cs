using CardosoRestaurante.Services.Carrinho.Models.Dto;

namespace CardosoRestaurante.Services.CarrinhoAPI.Service.IService
{
    //Vai receber a interface IProductRepository
    public interface IProdutoService
    {        ///</summary>
             /// Este método faz uma solicitação GET para a API de produtos, obtém a resposta, verifica se a solicitação foi bem-sucedida e retorna a lista de produtos se for o caso, ou uma lista vazia caso contrário.
             /// </summary>
             /// <returns></returns>
        Task<IEnumerable<ProdutoDto>> GetProdutos();
    }
}