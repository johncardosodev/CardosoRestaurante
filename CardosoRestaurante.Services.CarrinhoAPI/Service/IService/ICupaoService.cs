using CardosoRestaurante.Services.Carrinho.Models.DTO;

namespace CardosoRestaurante.Services.CarrinhoAPI.Service.IService
{
    public interface ICupaoService
    {
        Task<CupaoDto> GetCupao(string cupaoCodigo);
    }
}
