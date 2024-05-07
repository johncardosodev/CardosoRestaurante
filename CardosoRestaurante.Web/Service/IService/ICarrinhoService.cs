using CardosoRestaurante.Web.Models;

namespace CardosoRestaurante.Web.Service.IService
{
    public interface ICarrinhoService
    {
        Task<ResponseDto?> GetCarrinhoPeloUserIdAsync(string userId);

        Task<ResponseDto?> AtualizaCarrinhoAsync(CarrinhoDto carrinhoDto);

        Task<ResponseDto?> RemoverItemCarrinhoAsync(int carrinhoDetalheId);

        Task<ResponseDto?> AplicarCupaoAsync(CarrinhoDto carrinhoDto);

        Task<ResponseDto?> EmailCarrinho(CarrinhoDto carrinhoDto);
    }
}