using CardosoRestaurante.Services.Carrinho.Models.Dto;

namespace CardosoRestaurante.Services.CarrinhoAPI.Models
{
    public class CarrinhoDetalhesDto
    {
        public int CarrinhoDetalhesId { get; set; }

        public int CarrinhoInfoId { get; set; }

        public CarrinhoInfoDto? CarrinhoInfo { get; set; }

        public int ProdutoId { get; set; }

        public ProdutoDto? productDto { get; set; }

        public int Quantidade { get; set; }
    }
}