namespace CardosoRestaurante.Web.Models
{
    /// <summary>
    /// Esta classe é usada para armazenar e transferir informações sobre um carrinho de compras em um aplicativo de restaurante. Ela fornece uma estrutura organizada para armazenar os detalhes do carrinho e facilita a transferência dessas informações entre diferentes partes do aplicativo.

    /// </summary>
    public class CarrinhoInfoDto
    {
        public int CarrinhoInfoId { get; set; }

        public string? UserId { get; set; }
        public string? CupaoCodigo { get; set; }

        public double Desconto { get; set; }

        public double CarrinhoTotal { get; set; }

        public string? Nome { get; set; }
        public string? UltimoNome { get; set; }
        public string? Telemovel { get; set; }
        public string? Email { get; set; }
    }
}