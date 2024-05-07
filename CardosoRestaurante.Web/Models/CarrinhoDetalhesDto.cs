namespace CardosoRestaurante.Web.Models
{
    public class CarrinhoDetalhesDto
    {
        public int CarrinhoDetalhesId { get; set; }

        public int CarrinhoInfoId { get; set; }

        public CarrinhoInfoDto? CarrinhoInfo { get; set; }

        public int ProdutoId { get; set; }

        public ProdutoDto? produtoDto { get; set; }

        public int Quantidade { get; set; }
    }
}