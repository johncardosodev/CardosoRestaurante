using CardosoRestaurante.Web.Models;

namespace CardosoRestaurante.Web.Service.IService
{
    public interface IProdutoService
    {
        Task<ResponseDto?> GetProdutoAsync(string produtoNome);

        Task<ResponseDto?> GetTodosProdutosAsync();

        Task<ResponseDto?> GetProdutoPorIdAsync(int produtoId);

        Task<ResponseDto?> CriarProdutoAsync(ProdutoDto produtoDto);

        Task<ResponseDto?> AtualizarProdutoAsync(ProdutoDto produtoDto);

        Task<ResponseDto?> ApagarProdutoAsync(int produtoId);
    }
}