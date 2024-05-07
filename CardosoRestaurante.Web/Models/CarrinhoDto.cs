namespace CardosoRestaurante.Web.Models
{
    public class CarrinhoDto
    {
        public CarrinhoInfoDto CarrinhoInfo { get; set; }
        public IEnumerable<CarrinhoDetalhesDto>? CarrinhoDetalhes { get; set; }
    }
}