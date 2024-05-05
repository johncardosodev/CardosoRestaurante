using CardosoRestaurante.Services.Carrinho.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardosoRestaurante.Services.CarrinhoAPI.Models
{
    /// <summary>
    /// Este modelo é utilizado para armazenar os detalhes de um carrinho de compras, incluindo o produto selecionado e a quantidade desse produto no carrinho.
    /// </summary>
    public class CarrinhoDetalhes
    {
        /// <summary>
        ///  Um identificador único para cada detalhe do carrinho.
        /// </summary>
        public int CarrinhoDetalhesId { get; set; }

        /// <summary>
        /// Um identificador que faz referência às informações gerais do carrinho.
        /// </summary>
        [ForeignKey("CarrinhoInfoId")]
        public int CarrinhoInfoId { get; set; }

        /// <summary>
        ///  Uma propriedade de navegação que representa as informações gerais do carrinho.
        /// </summary>
        public CarrinhoInfo CarrinhoInfo { get; set; }

        /// <summary>
        /// um identificador que faz referência ao produto associado a este detalhe do carrinho.
        /// </summary>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Uma propriedade que não é mapeada para o banco de dados e representa um objeto de transferência de dados (DTO) para o produto.
        /// </summary>
        [NotMapped]
        public ProdutoDto productDto { get; set; }

        /// <summary>
        /// Representa a quantidade do produto selecionado que está no carrinho.
        /// </summary>
        public int Quantidade { get; set; }
    }
}