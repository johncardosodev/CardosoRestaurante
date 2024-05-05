using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardosoRestaurante.Services.CarrinhoAPI.Models
{
    /// <summary>
    /// modelo define a estrutura de dados para representar as informações de um carrinho de compras, incluindo o identificador, o usuário associado, o código do cupom, o desconto aplicado e o valor total do carrinho
    /// </summary>
    public class CarrinhoInfo
    {
        [Key]
        public int CarrinhoInfoId { get; set; }

        public string? UserId { get; set; }
        public string? CupaoCodigo { get; set; }

        [NotMapped] //Atributo que não é mapeado para a base de dados
        public double Desconto { get; set; }

        [NotMapped] //Atributo que não é mapeado para a base de dados
        public double CarrinhoTotal { get; set; }
    }
}