namespace CardosoRestaurante.Services.CarrinhoAPI.Models.Dto
{
    public class CarrinhoDto
    {
        public CarrinhoInfoDto CarrinhoInfo { get; set; }
        public IEnumerable<CarrinhoDetalhesDto>? CarrinhoDetalhes { get; set; }
    }
}